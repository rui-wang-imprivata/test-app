using OpenTelemetry;
using OpenTelemetry.Contrib.Extensions.AWSXRay.Resources;
using OpenTelemetry.Contrib.Extensions.AWSXRay.Trace;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using TestAppApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IForecastService, ForecastService>();

// Add open telementry tracing configuration
builder.Services.AddOpenTelemetryTracing(builder =>
{
    builder
         .AddSource("test-app")
         .SetResourceBuilder(
             ResourceBuilder.CreateDefault()
                 .AddDetector(new AWSEKSResourceDetector())
                 .AddService("test-app"))
         .AddXRayTraceId()
         .AddAspNetCoreInstrumentation(
             options =>
             {
                 options.RecordException = true;
             })
         .AddConsoleExporter()
         .AddOtlpExporter();
});
Sdk.SetDefaultTextMapPropagator(new AWSXRayPropagator());

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
