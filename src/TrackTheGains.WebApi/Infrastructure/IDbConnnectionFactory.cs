using System.Data;

namespace TrackTheGains.WebApi.Infrastructure
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
