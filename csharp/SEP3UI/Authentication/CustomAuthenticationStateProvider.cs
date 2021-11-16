using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using SEP3Library.Model;
using SEP3UI.Data;


namespace SEP3UI.Authentication {
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider {
        private readonly IJSRuntime jsRuntime;
        private readonly ICustomerService customerService;
        private Customer cached;

        public CustomAuthenticationStateProvider(IJSRuntime jsRuntime, ICustomerService customerService) {
            this.jsRuntime = jsRuntime;
            this.customerService = customerService;
        }
        
        public override async Task<AuthenticationState> GetAuthenticationStateAsync() {
            ClaimsIdentity identity = new ClaimsIdentity();
            
            if (cached == null) {
                string json = await jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "currentUser");
                
                if (!string.IsNullOrEmpty(json)) {
                    cached = JsonSerializer.Deserialize<Customer>(json);
                    identity = SetupClaims(cached);
                }
            } else {
                identity = SetupClaims(cached);
            }

            ClaimsPrincipal cachedPrincipal = new ClaimsPrincipal(identity);
            return await Task.FromResult(new AuthenticationState(cachedPrincipal));
        }
        
        public async Task LoginAsync(string email, string password) {
            if (string.IsNullOrWhiteSpace(email)) throw new Exception("Please specify your email address");
            if (string.IsNullOrWhiteSpace(password)) throw new Exception("Please specify your password");
            
            ClaimsIdentity identity = new ClaimsIdentity();
            
            try {
                Customer customer = await customerService.GetCustomerAsync(email, password);
                identity = SetupClaims(customer);
                string data = JsonSerializer.Serialize(customer);
                await jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", data);
                cached = customer;
            } catch (Exception e) {
                throw e;
            }
            
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity))));
        }

        public async Task LogoutAsync() {
            cached = null;
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity());
            await jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", "");
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        private ClaimsIdentity SetupClaims(Customer customer) {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("Id", customer.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Email, customer.Email));
            
            ClaimsIdentity identity = new ClaimsIdentity(claims, "apiauth_type");
            return identity;
        }
    }
}