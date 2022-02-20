using KhodiAsp.Data;
using KhodiAsp.Repositories;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace KhodiAsp.Security
{
    public class TokenAuthenticatorProvider : OAuthAuthorizationServerProvider
    {
        readonly TokenRepository tokenRepo = new TokenRepository();

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            
            bool isAuthed = false;
            var userGuid = Guid.Parse(context.UserName);
            var tokenGuid = Guid.Parse(UtilMethods.Base64Decode(context.Password));

            isAuthed = tokenRepo.validateToken(tokenGuid);
           
            if (isAuthed)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, "client"));
                identity.AddClaim(new Claim("username", "client"));
                identity.AddClaim(new Claim(ClaimTypes.Name, "client"));
                context.Validated(identity);
            }
            else
            {
                context.SetError("invalid_grant", "Provided token is incorrect");
                return;
            }
        }
    }
}