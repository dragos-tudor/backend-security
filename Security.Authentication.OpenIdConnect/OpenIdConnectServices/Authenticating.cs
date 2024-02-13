using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using static Microsoft.AspNetCore.Authentication.AuthenticateResult;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static async Task<AuthenticateResult> AuthenticateOidc<TOptions> (
    HttpContext context,
    TOptions oidcOptions,
    OpenIdConnectConfiguration oidcConfiguration,
    PropertiesDataFormat propertiesDataFormat,
    StringDataFormat stringDataFormat,
    PostAuthorizationFunc<TOptions> postAuthorize,
    ExchangeCodeForTokensFunc<TOptions> exchangeCodeForTokens,
    AccessUserInfoFunc<TOptions> accessUserInfo)
  where TOptions: OpenIdConnectOptions
  {
    var authResult = await postAuthorize(context, oidcOptions, oidcConfiguration, propertiesDataFormat, stringDataFormat);
    if(authResult.Failure is not null) LogPostAuthorizationWithFailure(Logger, oidcOptions.SchemeName, authResult.Failure, context.TraceIdentifier);
    if(authResult.Failure is not null) return Fail(authResult.Failure);
    LogPostAuthorization(Logger, oidcOptions.SchemeName, context.TraceIdentifier);

    return default!;
  }
}