using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

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
      RemoteSignOutPath = new PathString("/signout-oidc"),
      SignedOutCallbackPath = new PathString("/signout-callback-oidc"),
      SignedOutRedirectUri = "/",

      TokenHandler = new JsonWebTokenHandler { MapInboundClaims = JwtSecurityTokenHandler.DefaultMapInboundClaims },
      TokenValidationParameters = new TokenValidationParameters() { ValidAudience = clientId },
      UsePkce = true
    };
}