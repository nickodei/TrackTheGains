using Xunit;

namespace TrackTheGains.WebAPI.Tests.Integration;

[CollectionDefinition("Api collection")]
public class SharedTestCollection : ICollectionFixture<WebApiFactory>
{
    public const string COLLECTION_DEFINITION = "Api collection";
}
