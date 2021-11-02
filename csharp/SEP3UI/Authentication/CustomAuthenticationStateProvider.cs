using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using SEP3Library.Model;


namespace SEP3UI.Authentication {
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider {
        private readonly IJSRuntime jsRuntime;
        private readonly IUserService userService;
        private User cachedUser;

        public CustomAuthenticationStateProvider(IJSRuntime jsRuntime, IUserService userService) {
            this.jsRuntime = jsRuntime;
            this.userService = userService;
        }
        
        public override Task<AuthenticationState> GetAuthenticationStateAsync() {
            throw new System.NotImplementedException();
        }
    }
}