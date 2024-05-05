
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
    AccessUserInfoFunc<TOptions> accessUserInfo)
  where TOptions: OAuthOptions
  {
    var cancellationToken = context.RequestAborted;
    var requestId = context.TraceIdentifier;
    var schemeName = authOptions.SchemeName;

    var authResult = postAuthorize(context, authOptions, propertiesDataFormat);
    if(IsFailurePostAuthorizationResult(authResult)) LogPostAuthorizationFailure(Logger, schemeName, authResult.Failure!, requestId);
    if(IsFailurePostAuthorizationResult(authResult)) return Fail(authResult.Failure!);
    LogPostAuthorization(Logger, schemeName, requestId);

    var authProperties = GetAutheticationProperties(authResult);
    var authorizationCode = GetPostAuthorizationCode(context.Request)!;
    var tokenResult = await exchangeCodeForTokens(authorizationCode, authProperties!, authOptions, httpClient, cancellationToken);
    if (IsFailureTokenResult(tokenResult)) LogExchangeCodeForTokensFailure(Logger, schemeName, tokenResult.Failure!, requestId);
    if (IsFailureTokenResult(tokenResult)) return Fail(tokenResult.Failure!);
    LogExchangeCodeForTokens(Logger, schemeName, requestId);

    var accessToken = GetAccessToken(tokenResult);
    var userInfoResult = await accessUserInfo(accessToken!, authOptions, httpClient, cancellationToken);
    if(IsFailureUserInfoResult(userInfoResult)) LogAccessUserInfoFailure(Logger, schemeName, userInfoResult.Failure!, requestId);
    if(IsFailureUserInfoResult(userInfoResult)) return Fail(userInfoResult.Failure!);
    LogAccessUserInfo(Logger, schemeName, requestId);

    if (ShouldCleanCodeChallenge(authOptions))
      RemoveAuthenticationPropertiesCodeVerifier(authProperties!);

    var principal = GetClaimsPrincipal(userInfoResult)!;
    LogAuthenticated(Logger, schemeName, GetPrincipalNameId(principal)!, requestId);
    return Success(CreateAuthenticationTicket(principal, authProperties, schemeName));
  }


  public static Task<AuthenticateResult> AuthenticateOAuth<TOptions> (
    HttpContext context,
    PostAuthorizationFunc<TOptions> postAuthorize,
    ExchangeCodeForTokensFunc<TOptions> exchangeCodeForTokens,
    AccessUserInfoFunc<TOptions> accessUserInfo)
  where TOptions: OAuthOptions =>
    AuthenticateOAuth(
      context,
      ResolveService<TOptions>(context),
      ResolvePropertiesDataFormat<TOptions>(context),
      ResolveHttpClient<TOptions>(context),
      postAuthorize,
      exchangeCodeForTokens,
      accessUserInfo);

}