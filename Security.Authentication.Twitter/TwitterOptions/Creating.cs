
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Twitter;

partial class TwitterFuncs {

  public static TwitterOptions CreateTwitterOptions (
    string consumerKey,
    string consumerSecret,
    string schemeName = TwitterDefaults.AuthenticationScheme) =>
      new () {
        AuthorizationEndpoint = TwitterDefaults.AuthorizationEndpoint,
        TokenEndpoint = TwitterDefaults.TokenEndpoint,
        UserInformationEndpoint = TwitterDefaults.UserInformationEndpoint,

        ClientId = consumerKey,
        ClientSecret = consumerSecret,

        ClaimActions = MapTwitterClaimActions(new ClaimActionCollection()),
        CallbackPath = new PathString(TwitterDefaults.CallbackPath),

        SchemeName = schemeName,
        Scope = new [] { "tweet.read", "users.read" },
        ScopeSeparator = ' ',
        UsePkce = true
      };

}