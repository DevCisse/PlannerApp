using Blazored.LocalStorage;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PlannerApp
{
    public partial class Program
    {
        public class AuthorizationMessageHandler : DelegatingHandler
        {
            private readonly ILocalStorageService storage;

            public AuthorizationMessageHandler(ILocalStorageService  storage)
            {
                this.storage = storage;
            }
            protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                if(await storage.ContainKeyAsync("access_token"))
                {
                    var token = await storage.GetItemAsStringAsync("access_token");
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer,", token);
                }

                Console.WriteLine("Authorization message handler called");

                return await base.SendAsync(request, cancellationToken);                
            }

        }
    }
}
