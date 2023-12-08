
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Google;

partial class Funcs {

  internal static GoogleOptions CreateGoogleOptions (
    IDataProtectionProvider dataProtectionProvider,
    string? schemeName = GoogleDefaults.AuthenticationScheme) =>
      CreateOAuthOptions<GoogleOptions>(dataProtectionProvider, schemeName)
      with {
        AuthorizationEndpoint = GoogleDefaults.AuthorizationEndpoint,
        ClaimActions = MapGoogleClaimActions(new ClaimActionCollection()),
        CallbackPath = new PathString(GoogleDefaults.CallbackPath),
        TokenEndpoint = GoogleDefaults.TokenEndpoint,
        UserInformationEndpoint = GoogleDefaults.UserInformationEndpoint,
        Scope = new [] { "openid", "profile", "email" },
        ScopeSeparator = ' '
      };

}