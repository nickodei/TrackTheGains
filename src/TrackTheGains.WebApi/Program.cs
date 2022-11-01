using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using TrackTheGains.WebApi.Infrastructure;
using TrackTheGains.WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument();

builder.Services.AddScoped<IWorkoutService, WorkoutService>();

builder.Services.AddSingleton<IDbConnectionFactory>(_ =>
    new NpgsqlConnectionFactory(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddDbContext<FitnessContext>((provider, opt) =>
{
    var connectionFactory = provider.GetRequiredService<IDbConnectionFactory>();
    opt.UseNpgsql((DbConnection)connectionFactory.CreateConnection());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi3();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }