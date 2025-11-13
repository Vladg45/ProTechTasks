using BenchmarkDotNet.Running;
using DriverFinder.Benchmarks;

var builder = WebApplication.CreateBuilder(args);

// Если передан аргумент --algbenchmarks или --scalbenchmarks, запускаем бенчмарки
if (args.Contains("--algbenchmarks"))
{
    BenchmarkRunner.Run<AlgorithmBenchmarks>();
    return;
}
else if (args.Contains("--scalbenchmarks"))
{
    BenchmarkRunner.Run<ScalabilityBenchmarks>();
    return;
}

// Иначе запускаем обычное WebAPI
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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