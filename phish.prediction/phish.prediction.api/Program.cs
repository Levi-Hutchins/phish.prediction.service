using phish.prediction.api.Features.Cloudflare;
using phish.prediction.lib.Configuration;
using phish.prediction.lib.Features.Cloudflare.Config;

var builder = WebApplication.CreateBuilder(args);

// Add configuration services
builder.Services.Configure<CloudflareConfig>(builder.Configuration.GetSection("Cloudflare"));
builder.Services.AddSingleton<CloudflareConfigService>();

// Add HttpClient
builder.Services.AddHttpClient();

// Add MediatR for Vertical Slice Architecture
builder.Services.AddMediatR(_ => 
    _.RegisterServicesFromAssembly(typeof(ScanUrlCommandHandler).Assembly));

// Add controllers
builder.Services.AddControllers();

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable Swagger and Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Phish Prediction API v1");
        c.RoutePrefix = string.Empty; // Serves Swagger UI at the app's root (e.g., http://localhost:5000/)
    });
}

// Map controllers
app.MapControllers();

app.Run();