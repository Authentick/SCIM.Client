using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Gatekeeper.SCIM.Client.Action;
using Gatekeeper.SCIM.Client.Result;
using Gatekeeper.SCIM.Client.Schema.Core20;
using SemanticComparison.Fluent;
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
            Guid userId = Guid.NewGuid();
            User user = new User
            {
                ExternalId = userId.ToString(),
                UserName = "Test1",
                Active = false,
            };
            CreateUserAction createUserAction = new CreateUserAction(user);
            CreateUserResult createUserResult = await client.PerformAction<CreateUserResult>(createUserAction);
            Assert.Equal(StateEnum.Success, createUserResult.ResultStatus);
            user.AsSource()
                .OfLikeness<User>()
                .Without(u => u.Id)
                .ShouldEqual(createUserResult.User);
            user.Id = createUserResult.User.Id;

            // A lookup now should show the user
            GetUsersResult secondGetUsersRes = (await client.PerformAction<GetUsersResult>(new GetUsersAction()));
            Assert.NotEmpty(secondGetUsersRes.Users);
            Assert.Equal(1, secondGetUsersRes.Users.Count());
            user.AsSource()
                .OfLikeness<User>()
                .ShouldEqual(secondGetUsersRes.Users.First());
            Assert.Equal(StateEnum.Success, secondGetUsersRes.ResultStatus);

            // Assign the ID from the SCIM system
            user.Id = secondGetUsersRes.Users.First().Id;

            // Update the user
            user.Active = true;
            UpdateUserAction updateUserAction = new UpdateUserAction(user);
            UpdateUserResult updateUserResult = await client.PerformAction<UpdateUserResult>(updateUserAction);
            Assert.Equal(StateEnum.Success, updateUserResult.ResultStatus);
            Assert.NotNull(updateUserResult.User);

            user.AsSource()
                .OfLikeness<User>()
                .ShouldEqual(updateUserResult.User);

            // Recreating the same user should fail
            createUserAction = new CreateUserAction(user);
            createUserResult = await client.PerformAction<CreateUserResult>(createUserAction);
            Assert.Equal(StateEnum.Failure, createUserResult.ResultStatus);

            // Deleting the user should work
            DeleteUserAction deleteUserAction = new DeleteUserAction(user);
            DeleteUserResult deleteUserResult = await client.PerformAction<DeleteUserResult>(deleteUserAction);
            Assert.Equal(StateEnum.Success, deleteUserResult.ResultStatus);
            GetUsersResult thirdGetUsersRes = (await client.PerformAction<GetUsersResult>(new GetUsersAction()));
            Assert.Empty(thirdGetUsersRes.Users);
        }
    }
}
