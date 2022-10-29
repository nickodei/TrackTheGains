using Docker.DotNet.Models;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Respawn;
using TrackTheGains.WebApi.Infrastructure;
using TrackTheGains.WebAPI.Tests.Integration.Helpers;
using Xunit;

namespace TrackTheGains.WebAPI.Tests.Integration
{
    public class WebApiFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private Respawner respawner;
        private readonly TestcontainerDatabase testcontainers = new TestcontainersBuilder<PostgreSqlTestcontainer>()
            .WithDatabase(new PostgreSqlTestcontainerConfiguration
            {
                Database = "TrackTheGains",
                Username = "postgres",
                Password = "postgres",
                Port = 6654
            })
            .Build();

        public string ConnectionString => testcontainers.ConnectionString;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                services.AddDbContext<FitnessContext>(opt => opt.UseNpgsql(testcontainers.ConnectionString));
            });
        }

        public async Task InitializeAsync()
        {
            await testcontainers.StartAsync();

            using var scope = Server.Services.CreateScope();
            respawner = await RespawnHelper.CreateRespawner(ConnectionString);
        }

        public async Task EnsureCleanDatabase()
        {
            await RespawnHelper.ResetDbAsync(respawner, ConnectionString);
        }

        public async new Task DisposeAsync()
        {
            await testcontainers.DisposeAsync();
        }
    }
}
