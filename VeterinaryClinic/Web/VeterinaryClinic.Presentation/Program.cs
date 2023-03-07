using Microsoft.Net.Http.Headers;
using System.Text.Json;
using VeterinaryClinic.Presentation.ServiceAppointment;
using VeterinaryClinic.Presentation.ServiceManagement;
using ServiceAppointment = VeterinaryClinic.Presentation.ServiceAppointment;
using ServiceManagement = VeterinaryClinic.Presentation.ServiceManagement;

var builder = WebApplication.CreateBuilder(args);

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
    .Build();

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase);


//------------------------  REGISTER SERVICE FOR APPOINTMENT --------------------------
builder.Services.AddHttpClient("HttpClientAppointment", httpClient =>
{
    httpClient.BaseAddress = new Uri(config["ServicesUrls:AppointmentApi"]);
    httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");    
});

builder.Services.AddScoped<IHttpServiceAppointment, HttpServiceAppointment>();

builder.Services.AddScoped<ServiceAppointment.DoctorService>();
builder.Services.AddScoped<ServiceAppointment.ClientService>();
builder.Services.AddScoped<ServiceAppointment.PatientService>();
builder.Services.AddScoped<ServiceAppointment.RoomService>();
builder.Services.AddScoped<ServiceAppointment.AppointmentService>();
builder.Services.AddScoped<ServiceAppointment.ScheduleService>();
builder.Services.AddScoped<ServiceAppointment.AppointmentTypeService>();
builder.Services.AddScoped<ServiceAppointment.FileService>();
builder.Services.AddScoped<ServiceAppointment.ConfigurationService>();

//------------------------  REGISTER SERVICE FOR MANAGEMENT ---------------------------
builder.Services.AddHttpClient("HttpClientManagement", httpClient =>
{
    httpClient.BaseAddress = new Uri(config["ServicesUrls:ManagementApi"]);
    httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
});

builder.Services.AddScoped<IHttpServiceManagement, HttpServiceManagement>();

builder.Services.AddScoped<ServiceManagement.DoctorService>();
builder.Services.AddScoped<ServiceManagement.ClientService>();
builder.Services.AddScoped<ServiceManagement.PatientService>();
builder.Services.AddScoped<ServiceManagement.RoomService>();
builder.Services.AddScoped<ServiceManagement.AppointmentTypeService>();
builder.Services.AddScoped<ServiceManagement.FileService>();
builder.Services.AddScoped<ServiceManagement.ConfigurationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Appointment}/{action=Index}/{id?}");

app.Run();
