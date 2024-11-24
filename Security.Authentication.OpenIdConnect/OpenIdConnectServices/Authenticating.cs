using System.Net.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using static Microsoft.AspNetCore.Authentication.AuthenticateResult;
#nullable disable

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static async Task<AuthenticateResult> AuthenticateOidc<TOptions>(
    HttpContext context,
    TOptions oidcOptions,
    HttpClient httpClient,
    PropertiesDataFormat propertiesDataFormat,
    StringDataFormat stringDataFormat,
    PostAuthorizationFunc<TOptions> postAuthorize,
    ExchangeCodeForTokensFunc<TOptions> exchangeCodeForTokens,
    AccessUserInfoFunc<TOptions> accessUserInfo,
    ILogger logger)
  where TOptions: OpenIdConnectOptions
  {
    var (authProps, oidcData, authError) = await postAuthorize(context, oidcOptions, propertiesDataFormat, stringDataFormat);
    if(authError is not null) return Fail(authError);
    LogPostAuthorization(logger, oidcOptions.SchemeName, context.TraceIdentifier);

    var authCode = GetOidcDataAuthorizationCode(oidcData);
    var (tokenInfo, tokenError) = await exchangeCodeForTokens(authCode, authProps, oidcOptions, stringDataFormat, httpClient, context.RequestAborted);
    if(tokenError is not null) return Fail(tokenError);
    LogExchangeCodeForTokens(logger, oidcOptions.SchemeName, context.TraceIdentifier);

    var idToken = tokenInfo.IdToken;
    if(ShouldCleanCodeChallenge(oidcOptions)) UnsetAuthPropsCodeVerifier(authProps);
    if(ShouldSaveTokens(oidcOptions)) SetAuthPropsTokens(authProps, idToken, tokenInfo);

    if(ShouldAccessUserInfo(oidcOptions)) {
      var () = await accessUserInfo(tokenInfo!.AccessToken!, securityToken, identity, oidcOptions, httpClient, context.RequestAborted);
      if(userInfoResult.Error is not null) return Fail(userInfoResult.Error);
      LogAccessUserInfo(logger, oidcOptions.SchemeName, context.TraceIdentifier);
    }

    var principal = userInfoResult is not null?
      GetUserInfoResultPrincipal(userInfoResult):
      BuildClaimsPrincipal(oidcOptions, identity, "{}");
    return Success(CreateAuthenticationTicket(principal, authProps, oidcOptions.SchemeName));
  }
}