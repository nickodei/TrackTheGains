using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;
using Respawn;
using Respawn.Graph;
using System.Data.Common;
using TrackTheGains.WebApi.Infrastructure;
using Xunit;

namespace TrackTheGains.WebAPI.Tests.Integration
{
    public class WebApiFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private Respawner _respawner = default!;
        private DbConnection _dbConnection = default!;
        private readonly TestcontainerDatabase _testContainers = new TestcontainersBuilder<PostgreSqlTestcontainer>()
            .WithDatabase(new PostgreSqlTestcontainerConfiguration
            {
                Database = "TrackTheGains",
                Username = "postgres",
                Password = "postgres"
            })
            .Build();

        public HttpClient HttpClient { get; private set; } = default!;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                services.RemoveAll(typeof(IDbConnectionFactory));
                services.AddSingleton<IDbConnectionFactory>(_ =>
                    new NpgsqlConnectionFactory(_testContainers.ConnectionString));
            });
        }

        public async Task InitializeAsync()
        {
            await _testContainers.StartAsync();
            _dbConnection = new NpgsqlConnection(_testContainers.ConnectionString);
            HttpClient = CreateClient();
            await _dbConnection.OpenAsync();
            _respawner = await Respawner.CreateAsync(_dbConnection, new RespawnerOptions()
            {
                SchemasToExclude = new string[] { "public" },
                SchemasToInclude = new string[] { "fitness" },
                TablesToIgnore = new Table[]
                {
                    "__EFMigrationsHistory"
                },
                DbAdapter = DbAdapter.Postgres
            });
        }

        public async Task ResetDatabaseAsync()
        {
            await _respawner.ResetAsync(_dbConnection);
        }

        public async new Task DisposeAsync()
        {
            await _testContainers.DisposeAsync();
            await _dbConnection.DisposeAsync();
        }
    }
}
