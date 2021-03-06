using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using PlannerApp.Client.Services;
using PlannerApp.Client.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PlannerApp
{
    public partial class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddHttpClient("PlannerApp.Api", client =>
            {
                client.BaseAddress = new Uri("https://plannerapp-api.azurewebsites.net/");
                //  client.DefaultRequestVersion = new Version()
            }).AddHttpMessageHandler<AuthorizationMessageHandler>();
            //  builder.Services.AddTransient<AuthorizationMessageHandler>();

            //  builder.Services.AddScoped(sp => sp.GetService<IHttpClientFactory>().CreateClient("PlannerApp.Api"));
            //  builder.Services.AddMudServices();



            //  //allows the use of Authorize attribute
            //  builder.Services.AddAuthorizationCore();

            //  builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthenticationStateProvider>();




            //  builder.Services.AddBlazoredLocalStorage();

            ////  builder.Services.AddScoped<IAuthenticationService, HttpAuthenticationService>();
            //  builder.Services.AddHttpClientServices();

            builder.Services.AddTransient<AuthorizationMessageHandler>();

            builder.Services.AddScoped(sp => sp.GetService<IHttpClientFactory>().CreateClient("PlannerApp.Api"));

            builder.Services.AddMudServices();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthenticationStateProvider>();
            builder.Services.AddHttpClientServices();

            await builder.Build().RunAsync();
        }
    }
}
