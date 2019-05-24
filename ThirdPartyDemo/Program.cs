using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace ThirdPartyDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //Client();
            PwdClient();
        }

        private static void Client()
        {
            var client = new HttpClient();
            //var disco = client.GetDiscoveryDocumentAsync("http://localhost:5000").Result;
            //if (disco.IsError)
            //{
            //    Console.WriteLine(disco.Error);
            //}
            //var tokenClient = new TokenClient(disco.TokenEndpoint, "client", "secret");
            //var tokenResponse = tokenClient.RequestClientCredentialsAsync("api").Result;
            var response = client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = "http://localhost:5000/connect/token",
                ClientId = "client",
                ClientSecret = "secret",
                Scope = "api"
            }).Result;
            if (response.IsError)
            {
                Console.WriteLine(response.Error);
            }
            else
            {
                Console.WriteLine(response.Json);
            }
            var httpClient = new HttpClient();
            httpClient.SetBearerToken(response.AccessToken);
            var kost = httpClient.GetAsync("http://localhost:5001/api/values").Result;
            if (kost.IsSuccessStatusCode)
            {
                Console.WriteLine(kost.Content.ReadAsByteArrayAsync().Result);
            }
            Console.ReadLine();
        }

        private static void PwdClient()
        {
            var client = new HttpClient();
            var disco = client.GetDiscoveryDocumentAsync("http://localhost:5000").Result;
            var response = client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "pwdClient",
                ClientSecret = "secret",
                Scope = "api",
                UserName = "xcz",
                Password = "123456"
            }).Result;
            if (response.IsError)
            {
                Console.WriteLine(response.Error);
            }
            else
            {
                Console.WriteLine(response.Json);
            }
            var httpClient = new HttpClient();
            httpClient.SetBearerToken(response.AccessToken);
            var kost = httpClient.GetAsync("http://localhost:5001/api/values").Result;
            if (kost.IsSuccessStatusCode)
            {
                Console.WriteLine(kost.Content.ReadAsByteArrayAsync().Result);
            }
            Console.ReadLine();
        }
    }
}
