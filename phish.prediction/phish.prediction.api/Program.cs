using phish.prediction.api.Features.Cloudflare;
using phish.prediction.lib.Features.Cloudflare.Config;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<CloudflareMongoDB>
    (builder.Configuration.GetSection("Cloudflare-Mongo"));
builder.Services.Configure<CloudflareConfiguration>
    (builder.Configuration.GetSection("Cloudflare"));
builder.Services.Configure<Authentication>
    (builder.Configuration.GetSection("Authentication"));

builder.Services.AddSingleton<CloudflareService>();
builder.Services.AddHttpClient();

builder.Services.AddMediatR(_ => 
    _.RegisterServicesFromAssembly(typeof(ScanUrlCommandHandler).Assembly));

// chnage this (only for dev)
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAllOrigins",
        configurePolicy: policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors("AllowAllOrigins");

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

app.UseMiddleware<AuthenticationMiddleware>();
app.MapControllers();

app.Run();