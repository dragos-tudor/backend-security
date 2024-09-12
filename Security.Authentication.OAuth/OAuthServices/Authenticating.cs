
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
    if(IsFailurePostAuthorizationResult(authResult)) LogPostAuthorizationFailure(ResolveOAuthLogger(context), schemeName, authResult.Failure!, requestId);
    if(IsFailurePostAuthorizationResult(authResult)) return Fail(authResult.Failure!);
    LogPostAuthorization(ResolveOAuthLogger(context), schemeName, requestId);

    var authProperties = GetAutheticationProperties(authResult);
    var authorizationCode = GetPostAuthorizationCode(context.Request)!;
    var tokenResult = await exchangeCodeForTokens(authorizationCode, authProperties!, authOptions, httpClient, cancellationToken);
    if (IsFailureTokenResult(tokenResult)) LogExchangeCodeForTokensFailure(ResolveOAuthLogger(context), schemeName, tokenResult.Failure!, requestId);
    if (IsFailureTokenResult(tokenResult)) return Fail(tokenResult.Failure!);
    LogExchangeCodeForTokens(ResolveOAuthLogger(context), schemeName, requestId);

    var accessToken = GetAccessToken(tokenResult);
    var userInfoResult = await accessUserInfo(accessToken!, authOptions, httpClient, cancellationToken);
    if(IsFailureUserInfoResult(userInfoResult)) LogAccessUserInfoFailure(ResolveOAuthLogger(context), schemeName, userInfoResult.Failure!, requestId);
    if(IsFailureUserInfoResult(userInfoResult)) return Fail(userInfoResult.Failure!);
    LogAccessUserInfo(ResolveOAuthLogger(context), schemeName, requestId);

    if (ShouldCleanCodeChallenge(authOptions))
      RemoveAuthenticationPropertiesCodeVerifier(authProperties!);

    var principal = GetClaimsPrincipal(userInfoResult)!;
    LogAuthenticated(ResolveOAuthLogger(context), schemeName, GetPrincipalNameId(principal)!, requestId);
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
      ResolveRequiredService<TOptions>(context),
      ResolvePropertiesDataFormat<TOptions>(context),
      ResolveHttpClient<TOptions>(context),
      postAuthorize,
      exchangeCodeForTokens,
      accessUserInfo);

}