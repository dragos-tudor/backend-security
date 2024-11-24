using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static Task<string?> SignOutOidc<TOptions>(
    HttpContext context,
    SignOutFunc signOut,
    AuthenticationProperties authProps,
    ILogger logger)
  where TOptions : OpenIdConnectOptions =>
    SignOutOidc(
      context,
      authProps,
      ResolveRequiredService<TOptions>(context),
      signOut,
      logger
    );
}