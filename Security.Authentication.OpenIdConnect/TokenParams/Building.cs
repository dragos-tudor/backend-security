
namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static OidcParams BuildTokenParams(AuthenticationProperties authProps, OpenIdConnectOptions oidcOptions, string authCode)
  {
    var oidcParams = CreateOidcParams();
    var callbackUri = GetAuthPropsRedirectUriForCode(authProps);
    var codeVerifier = GetCodeVerifier(oidcOptions, authProps);

    return SetTokenParams(oidcParams, oidcOptions, authCode, codeVerifier, callbackUri!);
  }
}