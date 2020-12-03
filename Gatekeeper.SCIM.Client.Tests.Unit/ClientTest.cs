using System;
using System.Threading.Tasks;
using Xunit;

namespace Gatekeeper.SCIM.Client.Tests.Unit
{
    public class ClientTest
    {
        [Fact]
        public async Task Test1()
        {
            Client client = new Client(new Uri("http://localhost:5001/scim/"));
        }
    }
}
