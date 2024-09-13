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
    AccessUserInfoFunc<TOptions> accessUserInfo,
    ILogger logger)
  where TOptions: OpenIdConnectOptions
  {
    var authResult = await postAuthorize(context, oidcOptions, oidcConfiguration, propertiesDataFormat, stringDataFormat);
    if(authResult.Failure is not null) LogPostAuthorizationFailure(logger, oidcOptions.SchemeName, authResult.Failure, context.TraceIdentifier);
    if(authResult.Failure is not null) return Fail(authResult.Failure);
    LogPostAuthorization(logger, oidcOptions.SchemeName, context.TraceIdentifier);

    var authInfo = GetPostAuthorizationInfo(authResult)!;
    var authProperties = authInfo.AuthProperties;
    var tokenResult = default(TokenResult);
    if(ShouldExchangeCodeForTokens(authInfo)) {
      tokenResult = await exchangeCodeForTokens(authInfo.Code!, authProperties, oidcOptions,
        oidcConfiguration, stringDataFormat, httpClient, GetRequestCookies(context.Request), context.RequestAborted);
      if(tokenResult.Failure is not null) LogExchangeCodeForTokensFailure(logger, oidcOptions.SchemeName, tokenResult.Failure, context.TraceIdentifier);
      if(tokenResult.Failure is not null) return Fail(tokenResult.Failure);
      LogExchangeCodeForTokens(logger, oidcOptions.SchemeName, context.TraceIdentifier);
    }

    var tokenInfo = GetTokenInfo(tokenResult);
    var idToken = GetIdToken(authInfo, tokenInfo);
    if (ShouldCleanNonce(oidcOptions)) CleanNonceCookie(context, oidcOptions);
    if (ShouldCleanCodeChallenge(oidcOptions)) RemoveAuthenticationPropertiesCodeVerifier(authProperties);
    if (ShouldSaveTokens(oidcOptions)) SetAuthenticationPropertiesTokens(authProperties, idToken, tokenInfo);

    var identity = GetClaimsIdentity(authInfo, tokenInfo);
    var securityToken = GetSecurityToken(authInfo, tokenInfo);
    var userInfoResult = default(UserInfoResult);
    if (ShouldAccessUserInfo(oidcOptions, oidcConfiguration, tokenInfo)) {
      userInfoResult = await accessUserInfo(tokenInfo!.AccessToken!, securityToken, identity, oidcOptions, oidcConfiguration, httpClient, context.RequestAborted);
      if (userInfoResult.Failure is not null) LogAccessUserInfoFailure(logger, oidcOptions.SchemeName, userInfoResult.Failure, context.TraceIdentifier);
      if (userInfoResult.Failure is not null) return Fail(userInfoResult.Failure);
      LogAccessUserInfo(logger, oidcOptions.SchemeName, context.TraceIdentifier);
    }

    var principal = userInfoResult is not null?
      GetUserInfoResultPrincipal(userInfoResult):
      BuildClaimsPrincipal(oidcOptions, identity, "{}");
    LogAuthenticated(logger, oidcOptions.SchemeName, GetPrincipalNameId(principal)!, context.TraceIdentifier);
    return Success(CreateAuthenticationTicket(principal, authProperties, oidcOptions.SchemeName));
  }

  static Task<AuthenticateResult> AuthenticateOidc<TOptions> (
    HttpContext context,
    ILogger logger)
  where TOptions: OpenIdConnectOptions =>
    AuthenticateOidc(
      context,
      ResolveRequiredService<TOptions>(context),
      ResolveRequiredService<OpenIdConnectConfiguration>(context),
      ResolveHttpClient(context),
      ResolvePropertiesDataFormat(context),
      ResolveStringDataFormat(context),
      PostAuthorization,
      ExchangeCodeForTokens,
      AccessUserInfo,
      logger
    );
}