using System;
using System.Threading;
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
    }
}
