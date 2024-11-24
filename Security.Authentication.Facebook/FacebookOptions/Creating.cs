
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Facebook;

partial class FacebookFuncs {

  public static FacebookOptions CreateFacebookOptions(
    string appId,
    string appSecret,
    string schemeName = FacebookDefaults.AuthenticationScheme) =>
      new() {
        AuthorizationEndpoint = FacebookDefaults.AuthorizationEndpoint,
        TokenEndpoint = FacebookDefaults.TokenEndpoint,
        UserInfoEndpoint = FacebookDefaults.UserInfoEndpoint,

        ClientId = appId,
        ClientSecret = appSecret,

        ChallengePath = new PathString(FacebookDefaults.ChallengePath),
        ClaimMappers = GetFacebookClaimMappers(),
        CallbackPath = new PathString(FacebookDefaults.CallbackPath),

        Fields = ["name", "email"],
        ResponseType = OAuthResponseType.Code,
        SchemeName = schemeName,

        Scope = ["email"],
        ScopeSeparator = ',',
        SendAppSecretProof = true
        // UsePkce = true
      };

}