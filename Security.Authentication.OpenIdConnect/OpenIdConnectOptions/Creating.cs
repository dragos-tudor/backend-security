
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

      CallbackPath = new PathString("/signin-oidc"),
      ChallengePath = new PathString("/challenge-oidc"),
      SignOutRemotePath = new PathString("/signout-oidc"),
      SignedOutCallbackPath = new PathString("/signout-callback-oidc"),
      SignedOutRedirectUri = "/",

      UsePkce = true
    };
}