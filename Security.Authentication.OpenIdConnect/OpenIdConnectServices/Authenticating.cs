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
    OpenIdConnectValidationOptions validationOptions,
    HttpClient httpClient,
    PropertiesDataFormat authPropsProtector,
    PostAuthorizeFunc<TOptions> postAuthorize,
    ExchangeCodeForTokensFunc<TOptions> exchangeCodeForTokens,
    AccessUserInfoFunc<TOptions> accessUserInfo,
    ILogger logger)
  where TOptions: OpenIdConnectOptions
  {
    var (authProps, code, authError) = await postAuthorize(context, oidcOptions, validationOptions, authPropsProtector);
    if (authError is not null) return Fail(authError);
    LogPostAuthorize(logger, oidcOptions.SchemeName, context.TraceIdentifier);

    var (tokens, idToken, tokenError) = await exchangeCodeForTokens(code, authProps, oidcOptions, validationOptions, httpClient, context.RequestAborted);
    if (tokenError is not null) return Fail(tokenError);
    LogExchangeCodeForTokens(logger, oidcOptions.SchemeName, context.TraceIdentifier);

    if (ShouldCleanCodeChallenge(oidcOptions)) RemoveAuthPropsCodeVerifier(authProps);
    if (ShouldSaveTokens(oidcOptions)) SetAuthPropsTokens(authProps, tokens!);

    if (ShouldGetUserInfoClaims(oidcOptions)) {
      var (claims, userInfoError) = await accessUserInfo(tokens!.AccessToken!, oidcOptions, validationOptions, idToken, httpClient, context.RequestAborted);
      if (userInfoError is not null) return Fail(userInfoError);

      // TODO: add claims to idToken and apply claims actions/mappers
      LogAccessUserInfo(logger, oidcOptions.SchemeName, context.TraceIdentifier);
    }

    var principal = CreatePrincipal(oidcOptions.SchemeName, idToken.Claims);
    var ticket = CreateAuthenticationTicket(principal, authProps, oidcOptions.SchemeName);
    return Success(ticket);
  }
}