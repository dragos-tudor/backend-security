
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static OpenIdConnectOptions CreateOpenIdConnectOptions(OpenIdConnectConfiguration oidcConfig, string clientId, string clientSecret) =>
    new() {
      ClientId = clientId,
      ClientSecret = clientSecret,

      AuthorizationEndpoint = oidcConfig.AuthorizationEndpoint,
      TokenEndpoint = oidcConfig.TokenEndpoint,
      UserInfoEndpoint = oidcConfig.UserInfoEndpoint,

      ChallengePath = new PathString("/challenge-oidc"),
      ChallengeSignOutPath = new PathString("/challenge-signout-oidc"),
      CallbackPath = new PathString("/callback-oidc"),
      CallbackSignOutPath = new PathString("/callback-signout-oidc"),
      SignOutPath = oidcConfig.EndSessionEndpoint,

      AuthenticationMethod = OpenIdConnectRedirectBehaviour.RedirectGet,
      ClaimActions = GetOpenIdConnectClaimActions(),
      CheckSessionIframe = oidcConfig.CheckSessionIframe,
      DisableTelemetry = false,

      Issuer = oidcConfig.Issuer,
      SigningKeys = oidcConfig.SigningKeys,

      Prompt = OpenIdConnectPrompt.Login,
      ResponseMode = OpenIdConnectResponseMode.FormPost,
      ResponseType = OpenIdConnectResponseType.Code,

      TokenHandler = new JsonWebTokenHandler{ MapInboundClaims = JwtSecurityTokenHandler.DefaultMapInboundClaims },
      TokenValidationParameters = new TokenValidationParameters() {
        IssuerSigningKeys = oidcConfig.SigningKeys,
        RequireSignedTokens = false, // http://openid.net/specs/openid-connect-core-1_0.html#IDTokenValidation
        ValidIssuer = oidcConfig.Issuer
      },

      SchemeName = OidcDefaults.AuthenticationScheme,
      Scope =  [OpenIdConnectScope.OpenId, OpenIdConnectScope.Profile],
      UsePkce = true
    };
}