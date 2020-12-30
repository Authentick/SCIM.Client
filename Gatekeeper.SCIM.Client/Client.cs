using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Gatekeeper.SCIM.Client.Action;
using Gatekeeper.SCIM.Client.Result;
using Gatekeeper.SCIM.Client.Schema.Common20;
using Gatekeeper.SCIM.Client.Schema.Core20;

namespace Gatekeeper.SCIM.Client
{
    public class Client
    {
        private readonly Uri _baseUri;
        private string? _authToken;

        public Client(Uri baseUri)
        {
            _baseUri = baseUri;
        }

        public void SetAuthToken(string authToken)
        {
            _authToken = authToken;
        }

        public async Task<TResult> PerformAction<TResult>(IAction action) where TResult : IResult, new()
        {
            HttpClient client = GetHttpClient();
            JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
            {
                IgnoreNullValues = true,
            };

            switch (action)
            {
                case CreateAction<User> createUserAction:
                    HttpResponseMessage response = await client.PostAsJsonAsync<User>("Users", createUserAction.Resource, jsonSerializerOptions);

                    if (response.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        return (TResult)(object)new CreateResult<User>
                        {
                            ResultStatus = StateEnum.Success,
                            Resource = await response.Content.ReadFromJsonAsync<User>(),
                        };
                    }
                    else
                    {
                        ErrorResult errorResult = await response.Content.ReadFromJsonAsync<ErrorResult>();
                       
                        return (TResult)(object)new CreateResult<User>
                        {
                            ResultStatus = StateEnum.Failure,
                            ErrorDetail = (errorResult.Detail != null) ? errorResult.Detail : null,
                            ScimType = (errorResult.ScimType != null) ? errorResult.ScimType : null,
                        };
                    }

                case CreateAction<Group> createGroupAction:
                    response = await client.PostAsJsonAsync<Group>("Groups", createGroupAction.Resource, jsonSerializerOptions);
                    System.Console.WriteLine(await response.Content.ReadAsStringAsync());

                    if (response.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        return (TResult)(object)new CreateResult<Group>
                        {
                            ResultStatus = StateEnum.Success,
                            Resource = await response.Content.ReadFromJsonAsync<Group>(),
                        };
                    }
                    else
                    {
                        return (TResult)(object)new CreateResult<Group>
                        {
                            ResultStatus = StateEnum.Failure,
                        };
                    }

                case GetAction<User> getUserAction:
                    User reply = await client.GetFromJsonAsync<User>("Users/" + getUserAction.Id);

                    if (reply != null)
                    {
                        return (TResult)(object)new GetResult<User>
                        {
                            ResultStatus = StateEnum.Success,
                            Resource = reply,
                        };
                    }

                    return (TResult)(object)new GetUsersResult { ResultStatus = StateEnum.Failure };

                case GetAction<Group> getGroupAction:
                    Group groupReply = await client.GetFromJsonAsync<Group>("Groups/" + getGroupAction.Id);

                    if (groupReply != null)
                    {
                        return (TResult)(object)new GetResult<Group>
                        {
                            ResultStatus = StateEnum.Success,
                            Resource = groupReply,
                        };
                    }

                    return (TResult)(object)new GetUsersResult { ResultStatus = StateEnum.Failure };

                case GetUsersAction getUsersAction:
                    FilterResponse? filterResponse = await client.GetFromJsonAsync<FilterResponse>("Users");

                    if (filterResponse != null)
                    {
                        return (TResult)(object)new GetUsersResult
                        {
                            ResultStatus = StateEnum.Success,
                            Users = filterResponse.Resources,
                        };
                    }

                    return (TResult)(object)new GetUsersResult { ResultStatus = StateEnum.Failure };
                case UpdateUserAction updateUserAction:
                    if (updateUserAction.User.Id == null)
                    {
                        throw new Exception("No user ID provided for user");
                    }

                    response = await client.PutAsJsonAsync<User>("Users/" + updateUserAction.User.Id, updateUserAction.User, jsonSerializerOptions);

                    return (TResult)(object)new UpdateUserResult
                    {
                        ResultStatus = StateEnum.Success,
                        User = await response.Content.ReadFromJsonAsync<User>(),
                    };

                case UpdateGroupAction updateGroupAction:
                    if (updateGroupAction.Group.Id == null)
                    {
                        throw new Exception("No user ID provided for group");
                    }

                    response = await client.PutAsJsonAsync<Group>("Groups/" + updateGroupAction.Group.Id, updateGroupAction.Group, jsonSerializerOptions);

                    return (TResult)(object)new UpdateGroupResult
                    {
                        ResultStatus = StateEnum.Success,
                        Group = await response.Content.ReadFromJsonAsync<Group>(),
                    };


                case DeleteUserAction deleteUserAction:
                    response = await client.DeleteAsync("Users/" + deleteUserAction.Id);

                    return (TResult)(object)new DeleteUserResult
                    {
                        ResultStatus = StateEnum.Success,
                    };

                case DeleteGroupAction deleteGroupAction:
                    response = await client.DeleteAsync("Groups/" + deleteGroupAction.Id);

                    return (TResult)(object)new DeleteGroupResult
                    {
                        ResultStatus = StateEnum.Success,
                    };
            }

            throw new NotImplementedException();
        }

        private HttpClient GetHttpClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = _baseUri;

            if (_authToken != null)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _authToken);
            }

            return client;
        }
    }
}
