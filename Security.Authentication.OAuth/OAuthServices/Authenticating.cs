
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
    var authResult = postAuthorize(context, authOptions, propertiesDataFormat);
    if(authResult.Failure is not null) LogPostAuthorizationWithFailure(Logger, authOptions.SchemeName, authResult.Failure, context.TraceIdentifier);
    if(authResult.Failure is not null) return Fail(authResult.Failure);
    LogPostAuthorization(Logger, authOptions.SchemeName, context.TraceIdentifier);

    var authProperties = GetAutheticationProperties(authResult);
    var authorizationCode = GetPostAuthorizationCode(context.Request)!;
    var tokenResult = await exchangeCodeForTokens(authorizationCode, authProperties!, authOptions, httpClient, context.RequestAborted);
    if (tokenResult.Failure is not null) LogExchangeCodeForTokensWithFailure(Logger, authOptions.SchemeName, tokenResult.Failure, context.TraceIdentifier);
    if (tokenResult.Failure is not null) return Fail(tokenResult.Failure);
    LogExchangeCodeForTokens(Logger, authOptions.SchemeName, context.TraceIdentifier);

    var accessToken = GetAccessToken(tokenResult);
    var userInfoResult = await accessUserInfo(accessToken!, authOptions, httpClient, context.RequestAborted);
    if(userInfoResult.Failure is not null) LogAccessUserInfoWithFailure(Logger, authOptions.SchemeName, userInfoResult.Failure, context.TraceIdentifier);
    if(userInfoResult.Failure is not null) return Fail(userInfoResult.Failure);
    LogAccessUserInfo(Logger, authOptions.SchemeName, context.TraceIdentifier);

    if (ShouldCleanCodeChallenge(authOptions)) RemoveAuthenticationPropertiesCodeVerifier(authProperties!);

    var principal = GetClaimsPrincipal(userInfoResult)!;
    LogAuthenticated(Logger, authOptions.SchemeName, GetPrincipalNameId(principal)!, context.TraceIdentifier);
    return Success(CreateAuthenticationTicket(principal, authProperties, authOptions.SchemeName));
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