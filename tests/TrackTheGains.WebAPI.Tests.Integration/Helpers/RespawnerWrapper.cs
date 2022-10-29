using Npgsql;
using Respawn;
using Respawn.Graph;

namespace TrackTheGains.WebAPI.Tests.Integration.Helpers
{
    public static class RespawnHelper
    {
        public static RespawnerOptions GetPostgresOptions()
        {
            return new RespawnerOptions()
            {
                SchemasToExclude = new string[] { "public" },
                SchemasToInclude = new string[] { "fitness" },
                TablesToIgnore = new Table[]
                {
                    "__EFMigrationsHistory"
                },
                DbAdapter = DbAdapter.Postgres
            };
        }

        public static async Task<Respawner> CreateRespawner(string connectionString)
        {
            Respawner respawner;
            using (var npgsqlConnection = new NpgsqlConnection(connectionString))
            {
                await npgsqlConnection.OpenAsync();
                respawner = await Respawner.CreateAsync(npgsqlConnection, GetPostgresOptions());
                await npgsqlConnection.CloseAsync();
            }

            return respawner;
        }

        public static async Task ResetDbAsync(Respawner respawner, string connectionString)
        {
            using (var npgsqlConnection = new NpgsqlConnection(connectionString))
            {
                await npgsqlConnection.OpenAsync();
                await respawner.ResetAsync(npgsqlConnection);
                await npgsqlConnection.CloseAsync();
            }
        }
    }
}
