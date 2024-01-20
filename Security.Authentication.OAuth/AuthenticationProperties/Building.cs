using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  static AuthenticationProperties BuildAuthenticationProperties<TOptions>(
    HttpContext context,
    TOptions authOptions,
    string callbackUrl,
    string correlationId) where TOptions : OAuthOptions
  {
    var authProperties = new AuthenticationProperties();

    SetAuthenticationPropertiesCorrelationId(authProperties, correlationId);
    SetAuthenticationPropertiesRedirectUri(authProperties, GetRequestQueryReturnUrl(context.Request, authOptions.ReturnUrlParameter)!);
    SetAuthenticationPropertiesCallbackUri(authProperties, callbackUrl);

    if (ShouldUseCodeChallenge(authOptions)){
      var codeVerifier = GenerateCodeVerifier();
      SetAuthenticationPropertiesCodeVerifier(authProperties, codeVerifier);
    }

    return authProperties;
  }
}