using System;
using System.Linq;
using System.Threading.Tasks;
using Gatekeeper.SCIM.Client.Action;
using Gatekeeper.SCIM.Client.Result;
using Gatekeeper.SCIM.Client.Schema.Core20;
using Xunit;

namespace Gatekeeper.SCIM.Client.Tests.Integration
{
    public class ClientTest : IClassFixture<ServerFixture>
    {
        [Fact]
        public async Task IntegrationTest()
        {
            string token = await ServerFixture.GetAuthenticationToken();

            Client client = new Client(new Uri("http://localhost:5000/scim/"));
            client.SetAuthToken(token);

            // Initially no user should exist
            GetUsersResult initialGetUsersRes = (await client.PerformAction<GetUsersResult>(new GetUsersAction()));
            Assert.Empty(initialGetUsersRes.Users);
            Assert.Equal(StateEnum.Success, initialGetUsersRes.ResultStatus);

            // Create a new user
            User user = new User
            {
                UserName = "Test1",
            };
            CreateUserAction createUserAction = new CreateUserAction(user);
            CreateUserResult createUserResult = await client.PerformAction<CreateUserResult>(createUserAction);
            Assert.Equal(StateEnum.Success, createUserResult.ResultStatus);

            // A lookup now should show the user
            GetUsersResult secondGetUsersRes = (await client.PerformAction<GetUsersResult>(new GetUsersAction()));
            Assert.NotEmpty(secondGetUsersRes.Users);
            Assert.Equal(1, secondGetUsersRes.Users.Count());
            Assert.Equal(user.UserName, secondGetUsersRes.Users.First().UserName);
            Assert.Equal(StateEnum.Success, secondGetUsersRes.ResultStatus);
        }
    }
}
