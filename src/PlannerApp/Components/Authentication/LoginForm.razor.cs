using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using PlannerApp.Shared.Models;
using PlannerApp.Shared.Responses;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PlannerApp.Components.Authentication
{
    public partial class LoginForm
    {

        [Inject]
        public HttpClient HttpClient{ get; set; }


        private LoginRequest model = new LoginRequest();

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider{ get; set; }

        [Inject]
        public ILocalStorageService StorageService{ get; set; }

        [Inject]
        public ISnackbar Snackbar{ get; set; }




        private bool isBusy = default;
        private string errorMessage = default;

        public async Task HandleSubmit()
        {
            isBusy = true;

            var response = await HttpClient.PostAsJsonAsync("/api/v2/Auth/Login", model);

            if(response.IsSuccessStatusCode)
            {

                var result = await response.Content.ReadFromJsonAsync<ApiResponse<LoginResult>>();
                await StorageService.SetItemAsStringAsync("access_token", result.Value.Token);
                await StorageService.SetItemAsync<DateTime>("expiry_date", result.Value.ExpiryDate);


                await AuthenticationStateProvider.GetAuthenticationStateAsync();

                NavigationManager.NavigateTo("/");

            }
            else
            {
                var errorResult = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                errorMessage = errorResult.Message;
                if(!string.IsNullOrEmpty(errorMessage))
                {
                    Snackbar.Add(errorMessage, Severity.Error);
                }
                
            }


            isBusy = false;
        }
    }
}
