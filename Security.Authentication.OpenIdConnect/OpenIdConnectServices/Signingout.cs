using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static async Task<string?> SignOutOidc<TOptions>(
    HttpContext context,
    AuthenticationProperties authProperties,
    TOptions oidcOptions,
    OpenIdConnectConfiguration oidcConfiguration,
    PropertiesDataFormat propertiesDataFormat,
    SignOutFunc signOut)
  where TOptions : OpenIdConnectOptions
  {
    var signoutParams = await GetRequestParams(context.Request, context.RequestAborted);
    var signoutMessage = CreateOpenIdConnectMessage(signoutParams);
    var principal = GetContextUser(context);

    var validateError = ValidateSignoutMessage(signoutMessage, principal);
    if (validateError is not null) LogSignedOutWithFailure(ResolveOpenIdConnectLogger(context), oidcOptions.SchemeName, validateError, context.TraceIdentifier);
    if (validateError is not null) return default;

    var redirectUri = await signOut(context, authProperties);
    LogSignedOut(ResolveOpenIdConnectLogger(context), oidcOptions.SchemeName, redirectUri!, context.TraceIdentifier);
    return redirectUri;
  }

  public static Task<string?> SignOutOidc<TOptions>(
    HttpContext context,
    SignOutFunc signOut,
    AuthenticationProperties? authProperties = default)
  where TOptions : OpenIdConnectOptions =>
    SignOutOidc(
      context,
      authProperties ?? CreateAuthenticationProperties(),
      ResolveService<TOptions>(context),
      ResolveService<OpenIdConnectConfiguration>(context),
      ResolvePropertiesDataFormat<TOptions>(context),
      signOut
    );
}