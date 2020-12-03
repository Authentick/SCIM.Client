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
                case CreateUserAction createUserAction:
                    HttpResponseMessage response = await client.PostAsJsonAsync<User>("Users", createUserAction.User, jsonSerializerOptions);
                    return (TResult)(object)new CreateUserResult { ResultStatus = StateEnum.Success };

                case GetUsersAction getUsersAction:
                    FilterResponse? filterResponse = await client.GetFromJsonAsync<FilterResponse>("Users");

                    if (filterResponse != null)
                    {
                        GetUsersResult result = new GetUsersResult
                        {
                            ResultStatus = StateEnum.Success,
                            Users = filterResponse.Resources,
                        };

                        return (TResult)(object)result;
                    }

                    return (TResult)(object)new GetUsersResult { ResultStatus = StateEnum.Failure };
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
