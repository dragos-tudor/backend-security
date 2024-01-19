
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Google;

partial class GoogleFuncs {

  public static GoogleOptions CreateGoogleOptions (
    string clientId,
    string clientSecret,
    string schemeName = GoogleDefaults.AuthenticationScheme) =>
      new () {
        AuthorizationEndpoint = GoogleDefaults.AuthorizationEndpoint,
        TokenEndpoint = GoogleDefaults.TokenEndpoint,
        UserInformationEndpoint = GoogleDefaults.UserInformationEndpoint,

        ClientId = clientId,
        ClientSecret = clientSecret,

        ChallengePath = new PathString(GoogleDefaults.ChallengePath),
        ClaimActions = MapGoogleClaimActions(new ClaimActionCollection()),
        CallbackPath = new PathString(GoogleDefaults.CallbackPath),

        SchemeName = schemeName,
        Scope = new [] { "openid", "profile", "email" },
        ScopeSeparator = ' '
      };

}