using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SecretSanta
{
    public class AuthProvider : AuthenticationStateProvider
    {
        private ClaimsIdentity _identity = new ClaimsIdentity();

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(_identity)));
        }

        public void AuthorizeUser(SecretSantaParticipant participant)
        {
            var claims = new[] { new Claim(ClaimTypes.Name, participant.PhoneNumber) };
            claims.Append(new Claim(ClaimTypes.Role, participant.Role));
            _identity = new ClaimsIdentity(claims, "apiauth_type");
            var user = new ClaimsPrincipal(_identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public void LogOutUser()
        {
            _identity = new ClaimsIdentity();
            var user = new ClaimsPrincipal(_identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }
    }
}
