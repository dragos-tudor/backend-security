
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

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

      AuthenticationMethod = OpenIdConnectRedirectBehaviour.RedirectGet,
      ClaimActions = GetOpenIdConnectClaimActions(),
      DisableTelemetry = false,

      Issuer = oidcConfig.Issuer,
      SigningKeys = oidcConfig.SigningKeys,

      Prompt = OpenIdConnectPrompt.Login,
      ResponseMode = OpenIdConnectResponseMode.FormPost,
      ResponseType = OpenIdConnectResponseType.Code,

      SchemeName = OidcDefaults.AuthenticationScheme,
      Scope =  [OpenIdConnectScope.OpenId, OpenIdConnectScope.Profile],
      UsePkce = true
    };
}