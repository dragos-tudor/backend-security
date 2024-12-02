
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static OidcParams BuildTokenParams(AuthenticationProperties authProps, OpenIdConnectOptions oidcOptions, string authCode) =>
    SetTokenParams(CreateOidcParams(), authProps, oidcOptions, authCode);
}