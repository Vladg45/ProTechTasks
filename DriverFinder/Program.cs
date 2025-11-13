using DriverFinder.Middleware;
using DriverFinder.Models;
using DriverFinder.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Driver Finder API",
        Version = "v1",
        Description = "API для поиска и управления водителями"
    });
});

// Загружаем настройки
var appSettings = builder.Configuration.GetSection("Settings").Get<AppSettings>()
    ?? new AppSettings { ParallelLimit = 10 };

builder.Services.AddSingleton<IDriverService, DriverService>();
builder.Services.AddSingleton<IRandomNumberService, RandomNumberService>();
builder.Services.AddSingleton<IRouteCalculatorService, RouteCalculatorService>();
builder.Services.AddSingleton<IDriverAssignmentService, DriverAssignmentService>();

// Регистрируем ограничитель запросов
builder.Services.AddSingleton<IRequestLimiter>(provider =>
{
    var logger = provider.GetRequiredService<ILogger<RequestLimiter>>();
    return new RequestLimiter(appSettings.ParallelLimit, logger);
});

// Регистрируем http client для random number api
builder.Services.AddHttpClient<IRandomNumberService, RandomNumberService>(client =>
{
    client.Timeout = TimeSpan.FromSeconds(5);
});

builder.Services.AddLogging();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Driver Finder API v1");
        c.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();
app.UseMiddleware<RequestLimitingMiddleware>();
app.UseAuthorization();
app.MapControllers();
app.Run();