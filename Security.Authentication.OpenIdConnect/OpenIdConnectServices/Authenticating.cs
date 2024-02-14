using System.Net.Http;
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
    HttpClient httpClient,
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

    var authInfo = GetPostAuthorizationInfo(authResult)!;
    var authProperties = authInfo.AuthProperties;

    var tokenResult = ExistsPostAuthorizationCode(authInfo) && !ExistsPostAuthorizationIdentity(authInfo)? // skip hybrid flow
      await exchangeCodeForTokens(authInfo.Code!, authProperties,
        oidcOptions, oidcConfiguration, stringDataFormat, httpClient, GetRequestCookies(context.Request), context.RequestAborted):
      default;
    if(tokenResult?.Failure is not null) LogExchangeCodeForTokensWithFailure(Logger, oidcOptions.SchemeName, tokenResult.Failure, context.TraceIdentifier);
    if(tokenResult?.Failure is not null) return Fail(tokenResult.Failure);
    LogExchangeCodeForTokens(Logger, oidcOptions.SchemeName, context.TraceIdentifier);

    var tokenInfo = GetTokenInfo(tokenResult);
    var identity = tokenInfo?.Identity ?? authInfo.Identity;

    // save tokens on authentication properties
    // access user info
    // clean nonce cookie and authetication properties [code verifier]

    return default!;
  }

  static Task<AuthenticateResult> AuthenticateOidc<TOptions> (
    HttpContext context)
  where TOptions: OpenIdConnectOptions =>
    AuthenticateOidc(
      context,
      ResolveService<TOptions>(context),
      ResolveService<OpenIdConnectConfiguration>(context),
      ResolveHttpClient<TOptions>(context),
      ResolvePropertiesDataFormat<TOptions>(context),
      ResolveStringDataFormat<TOptions>(context),
      PostAuthorization,
      ExchangeCodeForTokens,
      default!
    );
}