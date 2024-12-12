
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  public static OAuthParams BuildTokenParams(AuthenticationProperties authProps, OAuthOptions oauthOptions, string authCode)
  {
    var oauthParams = CreateOAuthParams();
    var callbackUrl = GetAuthPropsCallbackUri(authProps);
    var codeVerifier = ResolveCodeVerifier(oauthOptions, authProps);

    return SetTokenParams(oauthParams, oauthOptions, authCode, callbackUrl!, codeVerifier);
  }
}