using Microsoft.AspNetCore.Components;
using PlannerApp.Shared.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace PlannerApp.Components.Authentication
{
    public partial class LoginForm
    {

        [Inject]
        public HttpClient HttpClient{ get; set; }


        private LoginRequest model = new LoginRequest();


        public async Task HandleSubmit()
        {
             await Task.FromResult("Task done");
        }
    }
}
