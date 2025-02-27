using System.Net.Http;
using static Microsoft.AspNetCore.Authentication.AuthenticateResult;
#nullable disable

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static async Task<AuthenticateResult> AuthenticateOidc<TOptions>(
    HttpContext context,
    TOptions oidcOptions,
    OpenIdConnectValidationOptions validationOptions,
    HttpClient httpClient,
    PropertiesDataFormat authPropsProtector,
    PostAuthorizeFunc<TOptions> postAuthorize,
    ExchangeCodeForTokensFunc<TOptions> exchangeCodeForTokens,
    AccessUserInfoFunc<TOptions> accessUserInfo,
    ILogger logger)
  where TOptions : OpenIdConnectOptions
  {
    var (authProps, code, authError) = await postAuthorize(context, oidcOptions, validationOptions, authPropsProtector);
    if (authError is not null) return Fail(ToOAuthErrorString(authError));
    LogPostAuthorize(logger, oidcOptions.SchemeName, context.TraceIdentifier);

    var correlationId = GetAuthPropsCorrelationId(authProps);
    DeleteCorrelationCookie(context, oidcOptions, correlationId);
    RemoveAuthPropsCorrelationId(authProps);

    var (tokens, idToken, tokenError) = await exchangeCodeForTokens(code, authProps, oidcOptions, validationOptions, httpClient, context.RequestAborted);
    if (tokenError is not null) return Fail(ToOAuthErrorString(tokenError));
    LogExchangeCodeForTokens(logger, oidcOptions.SchemeName, context.TraceIdentifier);

    if (ShouldCleanCodeChallenge(oidcOptions)) RemoveAuthPropsCodeVerifier(authProps);
    if (ShouldSaveTokens(oidcOptions)) SetAuthPropsTokens(authProps, tokens!);
    if (ShouldUseTokenLifetime(oidcOptions)) SetAuthPropsTokenLifetime(authProps, idToken!);

    IEnumerable<Claim> userClaims = Array.Empty<Claim>();
    if (ShouldGetUserInfoClaims(oidcOptions)) {
      var (_userClaims, userInfoError) = await accessUserInfo(tokens!.AccessToken!, oidcOptions, idToken, httpClient, context.RequestAborted);
      if (userInfoError is not null) return Fail(ToOAuthErrorString(userInfoError));

      userClaims = _userClaims;
      LogAccessUserInfo(logger, oidcOptions.SchemeName, context.TraceIdentifier);
    }

    var allClaims = JoinUniqueClaims(idToken.Claims, userClaims);
    var claims = ApplyClaimActions(oidcOptions.ClaimActions, allClaims);
    var principal = CreatePrincipal(oidcOptions.SchemeName, claims);
    var ticket = CreateAuthenticationTicket(principal, authProps, oidcOptions.SchemeName);

    return Success(ticket);
  }
}