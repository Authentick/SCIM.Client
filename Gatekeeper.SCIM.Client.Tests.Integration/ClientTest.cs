using System;
using System.Threading.Tasks;
using Xunit;

namespace Gatekeeper.SCIM.Client.Tests.Integration
{
    public class ClientTest : IClassFixture<ServerFixture>
    {
        [Fact]
        public async Task Test1()
        {
            Client client = new Client(new Uri("http://localhost:5000/scim/"));
            await client.Connect();
        }
    }
}
