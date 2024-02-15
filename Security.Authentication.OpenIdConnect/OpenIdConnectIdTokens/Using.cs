
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static Task<TokenValidationResult> UseIdToken<TOptions>(
    string idToken,
    TOptions oidcOptions,
    TokenValidationParameters validationParameters)
  where TOptions : OpenIdConnectOptions =>
    ValidateIdToken(idToken, validationParameters);


  static Task<TokenValidationResult> UseImplicitOrHybridFlowIdToken<TOptions>(
    string idToken,
    TOptions oidcOptions,
    OpenIdConnectConfiguration oidcConfiguration) where TOptions : OpenIdConnectOptions
  {
    var validationParameters = CreateTokenValidationParameters(oidcOptions);
    SetValidationParametersForIdTokenValidation(validationParameters, oidcConfiguration);

    return UseIdToken(idToken, oidcOptions, validationParameters);
  }

  static Task<TokenValidationResult> UseCodeOrHybridFlowIdToken<TOptions>(
    string idToken,
    TOptions oidcOptions,
    OpenIdConnectConfiguration oidcConfiguration) where TOptions : OpenIdConnectOptions
  {
    var validationParameters = CreateTokenValidationParameters(oidcOptions);
    SetValidationParametersForIdTokenValidation(validationParameters, oidcConfiguration);
    SetValidationParametersRequireSignedTokens(validationParameters, false);

    return UseIdToken(idToken, oidcOptions, validationParameters);
  }
}