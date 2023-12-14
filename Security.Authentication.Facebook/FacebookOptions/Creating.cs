
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Facebook;

partial class FacebookFuncs {

  public static FacebookOptions CreateFacebookOptions (
    string appId,
    string appSecret,
    string schemeName = FacebookDefaults.AuthenticationScheme) =>
      new () {
        AuthorizationEndpoint = FacebookDefaults.AuthorizationEndpoint,
        TokenEndpoint = FacebookDefaults.TokenEndpoint,
        UserInformationEndpoint = FacebookDefaults.UserInformationEndpoint,

        ClientId = appId,
        ClientSecret = appSecret,

        ClaimActions = MapFacebookClaimActions(new ClaimActionCollection()),
        CallbackPath = new PathString(FacebookDefaults.CallbackPath),

        Fields = new [] { "name", "email" },
        SchemeName = schemeName,
        Scope = new [] { "email" },
        ScopeSeparator = ',',

        SendAppSecretProof = true
      };

}