
using System.Net.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using static Microsoft.AspNetCore.Authentication.AuthenticateResult;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  public static async Task<AuthenticateResult> AuthenticateOAuth<TOptions>(
    HttpContext context,
    TOptions oauthOptions,
    PropertiesDataFormat authPropsProtector,
    HttpClient httpClient,
    PostAuthorizeFunc<TOptions> postAuthorize,
    ExchangeCodeForTokensFunc<TOptions> exchangeCodeForTokens,
    AccessUserInfoFunc<TOptions> accessUserInfo,
    ILogger logger)
  where TOptions : OAuthOptions
  {
    var cancellationToken = context.RequestAborted;
    var requestId = context.TraceIdentifier;
    var schemeName = oauthOptions.SchemeName;

    var (authProps, code, authError) = postAuthorize(context, oauthOptions, authPropsProtector);
    if (authError is not null) return Fail(ToOAuthErrorQuery(authError));
    LogPostAuthorize(logger, schemeName, requestId);

    var (tokens, tokenError) = await exchangeCodeForTokens(code!, authProps!, oauthOptions, httpClient, cancellationToken);
    if (tokenError is not null) return Fail(ToOAuthErrorQuery(tokenError));
    LogExchangeCodeForTokens(logger, schemeName, requestId);

    if (ShouldCleanCodeChallenge(oauthOptions)) RemoveAuthPropsCodeVerifier(authProps!);
    if (ShouldSaveTokens(oauthOptions)) SetAuthPropsTokens(authProps!, tokens!);

    var accessToken = GetAccessToken(tokens!);
    var (userClaims, userInfoError) = await accessUserInfo(accessToken!, oauthOptions, httpClient, cancellationToken);
    if (userInfoError is not null) return Fail(ToOAuthErrorQuery(userInfoError!));
    LogAccessUserInfo(logger, schemeName, requestId);

    var claims = ApplyClaimActions(oauthOptions.ClaimActions, userClaims!);
    var principal = CreatePrincipal(schemeName, claims);
    return Success(CreateAuthenticationTicket(principal, authProps, schemeName));
  }
}