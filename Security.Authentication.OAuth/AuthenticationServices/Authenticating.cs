
using System.Net.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using static Microsoft.AspNetCore.Authentication.AuthenticateResult;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  public static async Task<AuthenticateResult> AuthenticateOAuthAsync<TOptions> (
    HttpContext context,
    TOptions authOptions,
    ISecureDataFormat<AuthenticationProperties> secureDataFormat,
    HttpClient httpClient,
    PostAuthorizeFunc<TOptions> postAuthorize,
    ExchangeCodeForTokensFunc<TOptions> exchangeCodeForTokens,
    AccessUserInfoFunc<TOptions> accessUserInfo)
  where TOptions: OAuthOptions
  {
    var (authProperties, authError) = postAuthorize(context, authOptions, secureDataFormat);
    if(authError is not null) LogPostAuthorizeWithFailure(Logger, authOptions.SchemeName, authError, context.TraceIdentifier);
    if(authError is not null) return Fail(authError);

    var authorizationCode = GetAuthorizationCode(context.Request)!;
    var tokenResult = await exchangeCodeForTokens(authOptions, authProperties!, authorizationCode, httpClient, context.RequestAborted);
    if (tokenResult.Failure is not null) LogExchangeCodeForTokensWithFailure(Logger, authOptions.SchemeName, tokenResult.Failure, context.TraceIdentifier);
    if (tokenResult.Failure is not null) return Fail(tokenResult.Failure);
    LogExchangeCodeForTokens(Logger, authOptions.SchemeName, context.TraceIdentifier);

    var accessToken = GetAccessToken(tokenResult);
    var userInfoResult = await accessUserInfo(authOptions, accessToken!, httpClient, context.RequestAborted);
    if(userInfoResult.Failure is not null) LogAccessUserInfoWithFailure(Logger, authOptions.SchemeName, userInfoResult.Failure, context.TraceIdentifier);
    if(userInfoResult.Failure is not null) return Fail(userInfoResult.Failure);
    LogAccessUserInfo(Logger, authOptions.SchemeName, context.TraceIdentifier);

    LogAuthenticated(Logger, authOptions.SchemeName, GetPrincipalNameId(userInfoResult.Principal)!, context.TraceIdentifier);
    return Success(CreateAuthenticationTicket(userInfoResult.Principal!, authProperties, authOptions.SchemeName));
  }


  public static Task<AuthenticateResult> AuthenticateOAuthAsync<TOptions> (
    HttpContext context,
    PostAuthorizeFunc<TOptions> postAuthorize,
    ExchangeCodeForTokensFunc<TOptions> exchangeCodeForTokens,
    AccessUserInfoFunc<TOptions> accessUserInfo)
  where TOptions: OAuthOptions =>
    AuthenticateOAuthAsync(
      context,
      ResolveService<TOptions>(context),
      ResolveService<ISecureDataFormat<AuthenticationProperties>>(context),
      ResolveService<HttpClient>(context),
      postAuthorize,
      exchangeCodeForTokens,
      accessUserInfo);

}