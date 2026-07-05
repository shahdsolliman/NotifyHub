using Hangfire;
using System.Diagnostics;
using System.Linq;
using NotifyHub.Application;
using NotifyHub.Infrastructure;
using NotifyHub.Infrastructure.Persistence;
using NotifyHub.Infrastructure.RealTime;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddPersistenceServices(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "NotifyHub API v1");
    // Optional: serve Swagger UI at the app's root
    // c.RoutePrefix = string.Empty;
});



app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseHangfireDashboard("/hangfire");

app.MapControllers();
app.MapHub<NotificationHub>("/hubs/notifications");

app.Run();
