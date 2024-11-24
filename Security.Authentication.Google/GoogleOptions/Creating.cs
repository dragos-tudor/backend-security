
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Google;

partial class GoogleFuncs {

  public static GoogleOptions CreateGoogleOptions(
    string clientId,
    string clientSecret,
    string schemeName = GoogleDefaults.AuthenticationScheme) =>
      new() {
        AuthorizationEndpoint = GoogleDefaults.AuthorizationEndpoint,
        TokenEndpoint = GoogleDefaults.TokenEndpoint,
        UserInfoEndpoint = GoogleDefaults.UserInfoEndpoint,

        ClientId = clientId,
        ClientSecret = clientSecret,

        ChallengePath = new PathString(GoogleDefaults.ChallengePath),
        ClaimMappers = GetGoogleClaimMappers(),
        CallbackPath = new PathString(GoogleDefaults.CallbackPath),

        ResponseType = OAuthResponseType.Code,
        SchemeName = schemeName,
        Scope = new [] { "openid", "profile", "email" },
        ScopeSeparator = ' ',
        // UsePkce = true
      };

}