using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static async Task<TokenValidationResult> UseIdToken<TOptions>(
    string idToken,
    AuthenticationProperties authProperties,
    TOptions oidcOptions,
    TokenValidationParameters validationParameters,
    JsonWebTokenHandler tokenHandler) where TOptions : OpenIdConnectOptions
  {
    var validationResult = await ValidateIdToken(idToken, tokenHandler, validationParameters);
    if (validationResult.Exception is not null)
      return validationResult;

    var securityToken = validationResult.SecurityToken;
    if (ShouldUseTokenLifetime(oidcOptions))
      SetAuthenticationPropertiesTokenLifetime(authProperties, securityToken!);

    return validationResult;
  }

  static Task<TokenValidationResult> UseImplicitOrHybridFlowIdToken<TOptions>(
    string idToken,
    AuthenticationProperties authProperties,
    TOptions oidcOptions,
    OpenIdConnectConfiguration oidcConfiguration,
    JsonWebTokenHandler tokenHandler) where TOptions : OpenIdConnectOptions
  {
    var validationParameters = CreateTokenValidationParameters(oidcOptions);
    SetValidationParametersForIdTokenValidation(validationParameters, oidcConfiguration);

    return UseIdToken(idToken, authProperties, oidcOptions, validationParameters, tokenHandler);
  }

  static Task<TokenValidationResult> UseCodeOrHybridFlowIdToken<TOptions>(
    string idToken,
    AuthenticationProperties authProperties,
    TOptions oidcOptions,
    OpenIdConnectConfiguration oidcConfiguration,
    JsonWebTokenHandler tokenHandler) where TOptions : OpenIdConnectOptions
  {
    var validationParameters = CreateTokenValidationParameters(oidcOptions);
    SetValidationParametersForIdTokenValidation(validationParameters, oidcConfiguration);
    SetValidationParametersRequireSignedTokens(validationParameters, false);

    return UseIdToken(idToken, authProperties, oidcOptions, validationParameters, tokenHandler);
  }
}