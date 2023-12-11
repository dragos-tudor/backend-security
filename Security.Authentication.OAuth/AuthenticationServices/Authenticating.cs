
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using static Microsoft.AspNetCore.Authentication.AuthenticateResult;

namespace Security.Authentication.OAuth;

partial class Funcs {

  public static async Task<AuthenticateResult> AuthenticateOAuthAsync<TOptions> (
    HttpContext context,
    TOptions authOptions,
    PostAuthorizeFunc<TOptions> postAuthorize,
    ExchangeCodeForTokensFunc<TOptions> exchangeCodeForTokens,
    AccessUserInfoFunc<TOptions> accessUserInfo) where TOptions: OAuthOptions
  {
    var (authProperties, authError) = postAuthorize(context, authOptions);
    if(authError is not null) LogPostAuthorizeWithFailure(Logger, authOptions.SchemeName, authError, context.TraceIdentifier);
    if(authError is not null) return Fail(authError);

    var authorizationCode = GetAuthorizationCode(context.Request)!;
    var tokenResult = await exchangeCodeForTokens(authOptions, authProperties!, authorizationCode, context.RequestAborted);
    if (tokenResult.Failure is not null) LogExchangeCodeForTokensWithFailure(Logger, authOptions.SchemeName, tokenResult.Failure, context.TraceIdentifier);
    if (tokenResult.Failure is not null) return Fail(tokenResult.Failure);
    LogExchangeCodeForTokens(Logger, authOptions.SchemeName, context.TraceIdentifier);

    var accessToken = GetAccessToken(tokenResult);
    var userInfoResult = await accessUserInfo(authOptions, accessToken!, context.RequestAborted);
    if(userInfoResult.Failure is not null) LogAccessUserInfoWithFailure(Logger, authOptions.SchemeName, userInfoResult.Failure, context.TraceIdentifier);
    if(userInfoResult.Failure is not null) return Fail(userInfoResult.Failure);
    LogAccessUserInfo(Logger, authOptions.SchemeName, context.TraceIdentifier);

    LogAuthenticated(Logger, authOptions.SchemeName, GetPrincipalNameId(userInfoResult.Principal)!, context.TraceIdentifier);
    return Success(CreateAuthenticationTicket(userInfoResult.Principal!, authProperties, authOptions.SchemeName));
  }

  public static async Task<bool> AuthenticateAndSignInOAuthAsync<TOptions> (
    HttpContext context,
    TOptions authOptions,
    Func<HttpContext, TOptions, Task<AuthenticateResult>> authFunc,
    Func<HttpContext, ClaimsPrincipal, AuthenticationProperties, AuthenticationTicket> signinFunc) where TOptions : OAuthOptions
  {
    if (IsOAuthCallbackPath(context, authOptions)) {
      var authenticateResult = await authFunc(context, authOptions);
      if (authenticateResult.Failure is not null) {
        SetResponseRedirect(context.Response, BuildAuthenticationErrorPath(authenticateResult.Failure));
        return true;
      }

      if (authenticateResult.Principal is not null) {
        signinFunc(context, authenticateResult.Principal, authenticateResult.Ticket!.Properties);
        SetResponseRedirect(context.Response, authenticateResult.Ticket!.Properties.RedirectUri!);
        return true;
      }
    }

    return false;
  }

}