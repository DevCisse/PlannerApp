using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PlannerApp
{
    public class JwtAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService storage;

        public JwtAuthenticationStateProvider()
        {
        }

        public JwtAuthenticationStateProvider(ILocalStorageService storage)
        {
            this.storage = storage;
        }
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            //  return new AuthenticationState(new ClaimsPrincipal(new List<ClaimsIdentity>)))
            if (await storage.ContainKeyAsync("access_token"))
            {
                var tokenAsString = await storage.GetItemAsStringAsync("access_token");

                //we are going to decode the token 
                var tokenHandler = new JwtSecurityTokenHandler();
                var token  = tokenHandler.ReadJwtToken(tokenAsString);
                var identiy = new ClaimsIdentity(token.Claims, "bearer");

                var user = new ClaimsPrincipal(identiy);

                var authState = new AuthenticationState(user);
                //Calls AuthenticationStateChanged
                NotifyAuthenticationStateChanged(Task.FromResult(authState));
                //
                return authState; 
            }

            return new AuthenticationState(new ClaimsPrincipal());//return an emtty auth state meaning the user is not logged ing
        }
    }
}
