using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static async Task<string?> SignOutOidc<TOptions>(
    HttpContext context,
    AuthenticationProperties authProperties,
    TOptions oidcOptions,
    SignOutFunc signOut,
    ILogger logger)
  where TOptions : OpenIdConnectOptions
  {
    var signoutParams = await GetRequestParams(context.Request, context.RequestAborted);
    var signoutMessage = CreateOpenIdConnectMessage(signoutParams);
    var principal = GetContextUser(context);

    var validateError = ValidateSignoutMessage(signoutMessage, principal);
    if (validateError is not null) LogSignedOutWithFailure(logger, oidcOptions.SchemeName, validateError, context.TraceIdentifier);
    if (validateError is not null) return default;

    var redirectUri = await signOut(context, authProperties);
    LogSignedOut(logger, oidcOptions.SchemeName, redirectUri!, context.TraceIdentifier);
    return redirectUri;
  }

  public static Task<string?> SignOutOidc<TOptions>(
    HttpContext context,
    SignOutFunc signOut,
    AuthenticationProperties authProperties,
    ILogger logger)
  where TOptions : OpenIdConnectOptions =>
    SignOutOidc(
      context,
      authProperties,
      ResolveRequiredService<TOptions>(context),
      signOut,
      logger
    );
}