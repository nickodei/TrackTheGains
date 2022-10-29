using NUnit.Framework;

namespace TrackTheGains.WebAPI.Tests.Integration
{
    [TestFixture]
    public class ApiTestClass
    {
        protected WebApiFactory Factory { get; } = new();

        [OneTimeSetUp]
        public async Task Init()
        {
            await Factory.InitializeAsync();
        }

        [SetUp]
        public void CreateNewDatabase()
        {
            Factory.CreateNewDatabase();
        }

        [OneTimeTearDown]
        public async Task CleanUp()
        {
            await Factory.DisposeAsync();
        }
    }
}
