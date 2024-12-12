
namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static OidcParams BuildTokenParams(AuthenticationProperties authProps, OpenIdConnectOptions oidcOptions, string authCode)
  {
    var oidcParams = CreateOidcParams();
    var redirectUri = GetAuthPropsRedirectUriForCode(authProps);
    var codeVerifier = ShouldUseCodeChallenge(oidcOptions) ? GetAuthPropsCodeVerifier(authProps) : default;

    return SetTokenParams(oidcParams, oidcOptions, authCode, codeVerifier, redirectUri!);
  }
}