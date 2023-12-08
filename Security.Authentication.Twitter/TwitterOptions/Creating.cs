
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Twitter;

partial class Funcs {

  internal static TwitterOptions CreateTwitterOptions (
    IDataProtectionProvider dataProtectionProvider,
    string? schemeName = TwitterDefaults.AuthenticationScheme) =>
      CreateOAuthOptions<TwitterOptions>(dataProtectionProvider, schemeName)
      with {
        AuthorizationEndpoint = TwitterDefaults.AuthorizationEndpoint,
        ClaimActions = MapTwitterClaimActions(new ClaimActionCollection()),
        CallbackPath = new PathString(TwitterDefaults.CallbackPath),
        TokenEndpoint = TwitterDefaults.TokenEndpoint,
        UserInformationEndpoint = TwitterDefaults.UserInformationEndpoint,
        Scope = new [] { "tweet.read", "users.read" },
        ScopeSeparator = ' ',
        UsePkce = true
      };

}