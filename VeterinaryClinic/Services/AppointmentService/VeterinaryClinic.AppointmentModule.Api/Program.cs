using Autofac;
using Autofac.Extensions.DependencyInjection;
using System.Reflection;
using VeterinaryClinic.AppointmentModule.Api;
using VeterinaryClinic.AppointmentModule.Api.Notification;
using VeterinaryClinic.AppointmentModule.Infrastructure;
using VeterinaryClinic.AppointmentModule.Infrastructure.Data;

const string CORS_POLICY = "CorsPolicy";

var builder = WebApplication.CreateBuilder(args);

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
    .Build();

builder.Host
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureServices(services => {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddSignalR();
        services.AddCors(options =>
        {
            options.AddPolicy(name: CORS_POLICY,
                                    builder =>
                                    {
                                        builder.WithOrigins(config["SignalR:WebUrl"]!);
                                        builder.SetIsOriginAllowed(origin => true);
                                        builder.AllowAnyMethod();
                                        builder.AllowAnyHeader();
                                        builder.AllowCredentials();
                                    });
        });
    })
    .ConfigureContainer<ContainerBuilder>(containerBuilder =>
     {           
        containerBuilder.RegisterModule(new IoCApiModule());
        containerBuilder.RegisterModule(new IoCInfrastructureModule(config));

        var _automapperScanAssemblies = new List<Assembly>() { Assembly.GetAssembly(typeof(Program))! };
        containerBuilder.RegisterModule(new AutoMapperModule(_automapperScanAssemblies));
     });

var app = builder.Build();

IoCApiModule.ConfigureIntegrationEvents(app.Services, config);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || 
    Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Docker")
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(CORS_POLICY);
app.UseAuthorization();
app.MapControllers();
app.MapHub<ScheduleHub>("/scheduleHub");

using (var scope = app.Services.CreateScope())
{
    var loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();
    var logger = loggerFactory.CreateLogger<Program>();
    try
    {
        var seedService = app.Services.GetRequiredService<AppDbContextSeed>();
        await seedService.SeedAsync(new OfficeSettings().TestDate);
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

app.Run();
