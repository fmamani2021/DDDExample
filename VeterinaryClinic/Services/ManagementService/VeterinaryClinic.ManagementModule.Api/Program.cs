using Autofac;
using Autofac.Extensions.DependencyInjection;
using System.Reflection;
using VeterinaryClinic.ManagementModule.Api;
using VeterinaryClinic.ManagementModule.Infrastructure;
using VeterinaryClinic.ManagementModule.Infrastructure.Data;

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
    })
    .ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {        
        containerBuilder.RegisterModule(new IoCApiModule());
        containerBuilder.RegisterModule(new IoCInfrastructureModule(config));

        var _automapperScanAssemblies = new List<Assembly>() { Assembly.GetAssembly(typeof(Program))! };
        containerBuilder.RegisterModule(new AutoMapperModule(_automapperScanAssemblies));
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() ||
    Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Docker")
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

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
