
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  public static OAuthParams BuildTokenParams(AuthenticationProperties authProps, OAuthOptions oauthOptions, string authCode)
  {
    var oauthParams = CreateOAuthParams();
    var callback = GetAuthPropsRedirectUriForCode(authProps);
    var codeVerifier = GetCodeVerifier(oauthOptions, authProps);

    return SetTokenParams(oauthParams, oauthOptions, authCode, codeVerifier, callback!);
  }
}