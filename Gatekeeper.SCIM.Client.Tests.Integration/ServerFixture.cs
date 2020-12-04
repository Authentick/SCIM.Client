using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.SCIM.WebHostSample;

namespace Gatekeeper.SCIM.Client.Tests.Integration
{
    public class ServerFixture
    {
        public ServerFixture()
        {
            StartServer();
        }

        private void StartServer()
        {
            Program program = new Program();
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;

                Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
                Environment.SetEnvironmentVariable("ASPNETCORE_URLS", "http://localhost:5000 ");

                

                Program.Main(new string[0]);
            }).Start();

            Thread.Sleep(2000);
        }

        public static async Task<string> GetAuthenticationToken()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5000/scim/");

            HttpResponseMessage response = await client.GetAsync("token");
            string responseBody = await response.Content.ReadAsStringAsync();

            TokenReply replyObj = JsonSerializer.Deserialize<TokenReply>(responseBody);

            if (replyObj != null && replyObj.Token != null)
            {
                return replyObj.Token;
            }

            throw new Exception("Reply body was not a valid object: " + responseBody);
        }

        private class TokenReply
        {
            [JsonPropertyName("token")]
            public string? Token { get; set; }
        }
    }
}
