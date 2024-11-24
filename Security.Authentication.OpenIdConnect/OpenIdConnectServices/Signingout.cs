using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static async Task<string?> SignOutOidc<TOptions>(
    HttpContext context,
    AuthenticationProperties authProps,
    TOptions oidcOptions,
    SignOutFunc signOut,
    ILogger logger)
  where TOptions : OpenIdConnectOptions
  {
    var responseParams = await GetHttpRequestParams(context.Request, context.RequestAborted);
    var authParams = ToOAuthParams(responseParams);
    var principal = GetContextUser(context);

    var validateError = ValidateSignoutMessage(signoutMessage, principal);
    if(validateError is not null) LogSignedOutWithFailure(logger, oidcOptions.SchemeName, validateError, context.TraceIdentifier);
    if(validateError is not null) return validateError;

    await signOut(context, authProps);
    LogSignedOut(logger, oidcOptions.SchemeName, redirectUri!, context.TraceIdentifier);
    return redirectUri;
  }
}