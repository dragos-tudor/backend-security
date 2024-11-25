
using System.Net.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using static Microsoft.AspNetCore.Authentication.AuthenticateResult;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  public static async Task<AuthenticateResult> AuthenticateOAuth<TOptions>(
    HttpContext context,
    TOptions authOptions,
    PropertiesDataFormat authPropsProtector,
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

    var (authProps, authError) = postAuthorize(context, authOptions, authPropsProtector);
    if (authError is not null) return Fail(authError);
    LogPostAuthorization(logger, schemeName, requestId);

    var authCode = GetAuthorizationCode(context.Request)!;
    var (tokenInfo, tokenError) = await exchangeCodeForTokens(authCode, authProps!, authOptions, httpClient, cancellationToken);
    if (tokenError is not null) return Fail(tokenError);
    LogExchangeCodeForTokens(logger, schemeName, requestId);

    var accessToken = GetAccessToken(tokenInfo!);
    var (userClaims, userInfoError) = await accessUserInfo(accessToken!, authOptions, httpClient, cancellationToken);
    if (userInfoError is not null) return Fail(userInfoError!);
    LogAccessUserInfo(logger, schemeName, requestId);

    if (ShouldCleanCodeChallenge(authOptions)) UnsetAuthPropsCodeVerifier(authProps!);

    var principal = CreatePrincipal(schemeName, userClaims);
    return Success(CreateAuthenticationTicket(principal, authProps, schemeName));
  }
}