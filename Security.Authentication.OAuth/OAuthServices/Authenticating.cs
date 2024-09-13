
using System.Net.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using static Microsoft.AspNetCore.Authentication.AuthenticateResult;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  public static async Task<AuthenticateResult> AuthenticateOAuth<TOptions> (
    HttpContext context,
    TOptions authOptions,
    PropertiesDataFormat propertiesDataFormat,
    HttpClient httpClient,
    PostAuthorizationFunc<TOptions> postAuthorize,
    ExchangeCodeForTokensFunc<TOptions> exchangeCodeForTokens,
    AccessUserInfoFunc<TOptions> accessUserInfo,
    ILogger logger)
  where TOptions: OAuthOptions
  {
    var cancellationToken = context.RequestAborted;
    var requestId = context.TraceIdentifier;
    var schemeName = authOptions.SchemeName;

    var authResult = postAuthorize(context, authOptions, propertiesDataFormat);
    if(IsPostAuthorizationResultFailure(authResult)) LogPostAuthorizationFailure(logger, schemeName, authResult.Failure!, requestId);
    if(IsPostAuthorizationResultFailure(authResult)) return Fail(authResult.Failure!);
    LogPostAuthorization(logger, schemeName, requestId);

    var authProperties = GetAutheticationProperties(authResult);
    var authorizationCode = GetPostAuthorizationCode(context.Request)!;
    var tokenResult = await exchangeCodeForTokens(authorizationCode, authProperties!, authOptions, httpClient, cancellationToken);
    if (IsTokenResultFailure(tokenResult)) LogExchangeCodeForTokensFailure(logger, schemeName, tokenResult.Failure!, requestId);
    if (IsTokenResultFailure(tokenResult)) return Fail(tokenResult.Failure!);
    LogExchangeCodeForTokens(logger, schemeName, requestId);

    var accessToken = GetAccessToken(tokenResult);
    var userInfoResult = await accessUserInfo(accessToken!, authOptions, httpClient, cancellationToken);
    if(IsUserInfoResultFailure(userInfoResult)) LogAccessUserInfoFailure(logger, schemeName, userInfoResult.Failure!, requestId);
    if(IsUserInfoResultFailure(userInfoResult)) return Fail(userInfoResult.Failure!);
    LogAccessUserInfo(logger, schemeName, requestId);

    if (ShouldCleanCodeChallenge(authOptions)) RemoveAuthenticationPropertiesCodeVerifier(authProperties!);

    var principal = GetClaimsPrincipal(userInfoResult)!;
    LogAuthenticated(logger, schemeName, GetPrincipalNameId(principal)!, requestId);
    return Success(CreateAuthenticationTicket(principal, authProperties, schemeName));
  }


  public static Task<AuthenticateResult> AuthenticateOAuth<TOptions> (
    HttpContext context,
    PostAuthorizationFunc<TOptions> postAuthorize,
    ExchangeCodeForTokensFunc<TOptions> exchangeCodeForTokens,
    AccessUserInfoFunc<TOptions> accessUserInfo,
    ILogger logger)
  where TOptions: OAuthOptions =>
    AuthenticateOAuth(
      context,
      ResolveRequiredService<TOptions>(context),
      ResolvePropertiesDataFormat(context),
      ResolveHttpClient(context),
      postAuthorize,
      exchangeCodeForTokens,
      accessUserInfo,
      logger);

}