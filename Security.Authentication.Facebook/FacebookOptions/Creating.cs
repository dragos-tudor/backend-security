
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

        ChallengePath = new PathString(FacebookDefaults.ChallengePath),
        ClaimActions = MapFacebookClaimActions(new ClaimActionCollection()),
        CallbackPath = new PathString(FacebookDefaults.CallbackPath),

        Fields = [ "name", "email" ],
        SchemeName = schemeName,
        Scope = ["email" ],
        ScopeSeparator = ',',

        SendAppSecretProof = true
      };

}