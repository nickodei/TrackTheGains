using Npgsql;
using System.Data;

namespace TrackTheGains.WebApi.Infrastructure
{
    public class NpgsqlConnectionFactory : IDbConnectionFactory
    {
        private readonly string? _connectionString;

        public NpgsqlConnectionFactory(string? connectionString)
        {
            this._connectionString = connectionString;
        }

        public IDbConnection CreateConnection()
        {
            if(_connectionString is null)
            {
                throw new NullReferenceException("The connection string was null");
            }
            return new NpgsqlConnection(_connectionString);
        }
    }
}
