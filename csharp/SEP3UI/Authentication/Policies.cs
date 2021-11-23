using Microsoft.AspNetCore.Authorization;

namespace SEP3UI.Authentication {
    public static class Policies {
        public const string IsAdmin = "IsAdmin";

        public static AuthorizationPolicy FollowAdminPolicy() {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
                .RequireRole("Admin")
                .Build();
        }
    }
}