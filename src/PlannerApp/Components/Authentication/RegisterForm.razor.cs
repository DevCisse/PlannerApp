using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using PlannerApp.Client.Services.Exceptions;
using PlannerApp.Client.Services.Interfaces;
using PlannerApp.Shared.Models;
using System;
using System.Threading.Tasks;

namespace PlannerApp.Components.Authentication
{
    public partial class RegisterForm
    {
        [Inject]
        public IAuthenticationService HttpClient { get; set; }


        private RegisterRequest model = new RegisterRequest();

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        public ILocalStorageService StorageService { get; set; }

        [Inject]
        public ISnackbar Snackbar { get; set; }




        private bool isBusy = default;
        private string errorMessage = default;

        public async Task HandleSubmit()
        {
            isBusy = true;


            try
            {
                var response = await HttpClient.RegisterUserAsync(model);
                NavigationManager.NavigateTo("/authentication/login");

            }
            catch (ApiException ex)
            {
                errorMessage = ex.ApiErrorResponse.Message;

            }
            catch (Exception ex)
            {

                errorMessage = ex.Message;
            }
            
            if(!string.IsNullOrEmpty(errorMessage))
            {
                Snackbar.Add(errorMessage, Severity.Error);
            }

            isBusy = false;
        }


        private void RedirectToLogin()
        {
            NavigationManager.NavigateTo("/authentication/login");
        }
    }
}
