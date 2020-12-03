using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Gatekeeper.SCIM.Client
{
    public class Client
    {
        private readonly Uri _baseUri;

        public Client(Uri baseUri)
        {
            _baseUri = baseUri;
        }

        public async Task Connect()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = _baseUri;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYmYiOjE2MDY5OTkxMzUsImV4cCI6MTYwNzAwNjMzNSwiaXNzIjoiTWljcm9zb2Z0LlNlY3VyaXR5LkJlYXJlciIsImF1ZCI6Ik1pY3Jvc29mdC5TZWN1cml0eS5CZWFyZXIifQ.ESWlAwS0Pos4ptENBHHsoH7pOWYzeGkudDS9avQF_i8");

            var task = await client.GetAsync("Users");
            System.Console.WriteLine(task);
        }
    }
}
