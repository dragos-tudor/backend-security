
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
    PostAuthorizeFunc<TOptions> postAuthorize,
    ExchangeCodeForTokensFunc<TOptions> exchangeCodeForTokens,
    AccessUserInfoFunc<TOptions> accessUserInfo,
    ILogger logger)
  where TOptions: OAuthOptions
  {
    var cancellationToken = context.RequestAborted;
    var requestId = context.TraceIdentifier;
    var schemeName = authOptions.SchemeName;

    var (authProps, code, authError) = postAuthorize(context, authOptions, authPropsProtector);
    if (authError is not null) return Fail(authError);
    LogPostAuthorize(logger, schemeName, requestId);

    var (tokens, tokenError) = await exchangeCodeForTokens(code!, authProps!, authOptions, httpClient, cancellationToken);
    if (tokenError is not null) return Fail(tokenError);
    LogExchangeCodeForTokens(logger, schemeName, requestId);

    if (ShouldCleanCodeChallenge(authOptions)) RemoveAuthPropsCodeVerifier(authProps!);

    var accessToken = GetAccessToken(tokens!);
    var (userClaims, userInfoError) = await accessUserInfo(accessToken!, authOptions, httpClient, cancellationToken);
    if (userInfoError is not null) return Fail(userInfoError!);
    LogAccessUserInfo(logger, schemeName, requestId);

    // TODO: apply claim actions/mappers
    var principal = CreatePrincipal(schemeName, userClaims);
    return Success(CreateAuthenticationTicket(principal, authProps, schemeName));
  }
}