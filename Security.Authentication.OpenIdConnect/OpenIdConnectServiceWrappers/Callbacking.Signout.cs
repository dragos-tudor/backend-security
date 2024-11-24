
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static string? CallbackSignoutOidc<TOptions>(
    HttpContext context,
    ILogger logger)
  where TOptions : OpenIdConnectOptions =>
    CallbackSignoutOidc(
      context,
      ResolveRequiredService<TOptions>(context),
      ResolvePropertiesDataFormat(context),
      logger);
}