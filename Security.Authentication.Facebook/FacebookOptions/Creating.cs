
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Facebook;

partial class Funcs {

  internal static FacebookOptions CreateFacebookOptions (
    IDataProtectionProvider dataProtectionProvider,
    string? schemeName = FacebookDefaults.AuthenticationScheme) =>
      CreateOAuthOptions<FacebookOptions>(dataProtectionProvider, schemeName)
      with {
        AuthorizationEndpoint = FacebookDefaults.AuthorizationEndpoint,
        ClaimActions = MapFacebookClaimActions(new ClaimActionCollection()),
        CallbackPath = new PathString(FacebookDefaults.CallbackPath),
        TokenEndpoint = FacebookDefaults.TokenEndpoint,
        UserInformationEndpoint = FacebookDefaults.UserInformationEndpoint,
        Fields = new [] { "name", "email" },
        Scope = new [] { "email" },
        ScopeSeparator = ',',
        SendAppSecretProof = true
      };

}