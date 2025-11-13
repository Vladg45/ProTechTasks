using BenchmarkDotNet.Running;
using DriverFinder.Benchmarks;
using DriverFinder.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IDriverService, DriverService>();
builder.Services.AddSingleton<IRandomNumberService, RandomNumberService>();
builder.Services.AddSingleton<IRouteCalculatorService, RouteCalculatorService>();
builder.Services.AddSingleton<IDriverAssignmentService, DriverAssignmentService>();

// Регистрируем http client для random number api
builder.Services.AddHttpClient<IRandomNumberService, RandomNumberService>(client =>
{
    client.Timeout = TimeSpan.FromSeconds(5);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();