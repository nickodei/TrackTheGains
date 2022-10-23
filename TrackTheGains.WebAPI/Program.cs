using MediatR;
using Microsoft.EntityFrameworkCore;
using TrackTheGains.Infrastructure;
using TrackTheGains.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddDbContext<FitnessContext>(opt =>
    {
        opt.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
    });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument();
builder.Services.AddMediatR(typeof(Program));

builder.Services.AddScoped<IWorkoutRepository, WorkoutRepository>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<FitnessContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi3();
}
else
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();

app.MapControllers();

app.Run();