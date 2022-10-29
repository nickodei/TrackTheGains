using Npgsql;
using Respawn;

namespace TrackTheGains.WebAPI.Tests.Integration.Helpers
{
    public static class RespawnerWrapper
    {
        public static async Task<Respawner> CreateCheckpoint(string connectionString)
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();

            return await Respawner.CreateAsync(connection, new RespawnerOptions
            {
                SchemasToExclude = new[]
                {
                    "public"
                },
                DbAdapter = DbAdapter.Postgres
            });
        }

        public static async Task ResetToCheckpoint(Respawner respawner, string connectionString)
        {
            using var conn = new NpgsqlConnection(connectionString);

            conn.Open();
            await respawner.ResetAsync(conn);
        }
    }
}
