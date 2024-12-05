
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  public static OAuthParams BuildTokenParams(AuthenticationProperties authProps, OAuthOptions oauthOptions, string authCode) =>
    SetTokenParams(CreateOAuthParams(), authProps, oauthOptions, authCode);
}