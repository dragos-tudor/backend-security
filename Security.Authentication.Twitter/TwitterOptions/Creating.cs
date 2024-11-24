using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Twitter;

partial class TwitterFuncs
{
  public static TwitterOptions CreateTwitterOptions(
    string consumerKey,
    string consumerSecret,
    string schemeName = TwitterDefaults.AuthenticationScheme) =>
      new() {
        AuthorizationEndpoint = TwitterDefaults.AuthorizationEndpoint,
        TokenEndpoint = TwitterDefaults.TokenEndpoint,
        UserInfoEndpoint = TwitterDefaults.UserInfoEndpoint,

        ClientId = consumerKey,
        ClientSecret = consumerSecret,

        ChallengePath = new PathString(TwitterDefaults.ChallengePath),
        ClaimMappers = GetTwitterClaimMappers(),
        CallbackPath = new PathString(TwitterDefaults.CallbackPath),

        ResponseType = OAuthResponseType.Code,
        SchemeName = schemeName,
        Scope = new [] { "tweet.read", "users.read" },
        ScopeSeparator = ' ',
        UsePkce = true
      };

}