
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static OpenIdConnectOptions CreateOpenIdConnectOptions(string clientId, string clientSecret) =>
    new () {
      ClientId = clientId,
      ClientSecret = clientSecret,

      AuthenticationMethod = OpenIdConnectRedirectBehaviour.RedirectGet,
      ClaimActions = MapOpenIdConnectClaimActions([]),
      NonceLifetime = TimeSpan.FromMinutes(15),
      DisableTelemetry = true,

      ResponseMode = OpenIdConnectResponseMode.FormPost,
      ResponseType = OpenIdConnectResponseType.IdToken,
      RequireNonce = true,
      Scope =  ["openid", "profile"],

      CallbackPath = new PathString("/callback-oidc"),
      ChallengePath = new PathString("/challenge-oidc"),
      ChallengeSignOutPath = new PathString("/signout-challenge-oidc"),
      CallbackSignOutPath = new PathString("/signout-callback-oidc"),
      SignOutRedirectUri = "/",

      UsePkce = true
    };
}