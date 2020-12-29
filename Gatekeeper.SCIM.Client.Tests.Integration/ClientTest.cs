using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
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
                Emails = new List<User.EmailAttribute>{
                    new User.EmailAttribute { Value = "test1@example.com" },
                    new User.EmailAttribute { Value = "test2@example.com" },
                },
            };
            CreateAction<User> createUserAction = new CreateAction<User>(user);
            CreateResult<User> createUserResult = await client.PerformAction<CreateResult<User>>(createUserAction);
            Assert.Equal(StateEnum.Success, createUserResult.ResultStatus);
            user.AsSource()
                .OfLikeness<User>()
                .Without(u => u.Id)
                .Without(u => u.Meta)
                .Without(u => u.Emails)
                .ShouldEqual(createUserResult.Resource);
            user.Emails.AsSource()
                .OfLikeness<List<User.EmailAttribute>>()
                .ShouldEqual(createUserResult.Resource.Emails);
            user.Id = createUserResult.Resource.Id;

            // A lookup now should show the user
            GetUsersResult secondGetUsersRes = (await client.PerformAction<GetUsersResult>(new GetUsersAction()));
            Assert.NotEmpty(secondGetUsersRes.Users);
            Assert.Equal(1, secondGetUsersRes.Users.Count());
            user.AsSource()
                .OfLikeness<User>()
                .Without(u => u.Meta)
                .Without(u => u.Emails)
                .ShouldEqual(secondGetUsersRes.Users.First());
            user.Emails.AsSource()
                .OfLikeness<List<User.EmailAttribute>>()
                .ShouldEqual(secondGetUsersRes.Users.First().Emails);
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
                .Without(u => u.Meta)
                .Without(u => u.Emails)
                .ShouldEqual(updateUserResult.User);
            user.Emails.AsSource()
                .OfLikeness<List<User.EmailAttribute>>()
                .ShouldEqual(updateUserResult.User.Emails);

            // Create a group with our only user inside
            Group group = new Group
            {
                ExternalId = Guid.NewGuid().ToString(),
                DisplayName = "My test group",
                Members = new List<Group.GroupMembership>() {
                    new Group.GroupMembership {
                        Value = createUserResult.Resource.Id,
                        Ref = createUserResult.Resource.Meta.Location,
                    },
                },
            };
            CreateAction<Group> createGroupAction = new CreateAction<Group>(group);
            CreateResult<Group> createGroupResult = await client.PerformAction<CreateResult<Group>>(createGroupAction);

            group.AsSource()
                .OfLikeness<Group>()
                .Without(g => g.Id)
                .Without(u => u.Meta)
                .Without(u => u.Members)
                .ShouldEqual(createGroupResult.Resource);

            group.Members.AsSource()
                .OfLikeness<IEnumerable<Group.GroupMembership>>()
                .ShouldEqual(createGroupResult.Resource.Members);

            // Single querying the user should work
            GetAction<User> getUserAction = new GetAction<User>(user.Id);
            GetResult<User> getUserResult = await client.PerformAction<GetResult<User>>(getUserAction);

            user.AsSource()
                .OfLikeness<User>()
                .Without(g => g.Meta)
                .Without(u => u.Emails)
                .ShouldEqual(getUserResult.Resource);
            user.Emails.AsSource()
                .OfLikeness<List<User.EmailAttribute>>()
                .ShouldEqual(getUserResult.Resource.Emails);

            // Single querying the group should work
            GetAction<Group> getGroupAction = new GetAction<Group>(createGroupResult.Resource.Id);
            GetResult<Group> getGroupResult = await client.PerformAction<GetResult<Group>>(getGroupAction);

            group.AsSource()
                .OfLikeness<Group>()
                .Without(g => g.Meta)
                .Without(g => g.Members)
                .Without(g => g.Id)
                .ShouldEqual(getGroupResult.Resource);

            group.Members.AsSource()
                .OfLikeness<IEnumerable<Group.GroupMembership>>()
                .ShouldEqual(getGroupResult.Resource.Members);

            // Recreating the same user should fail
            createUserAction = new CreateAction<User>(user);
            createUserResult = await client.PerformAction<CreateResult<User>>(createUserAction);
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
