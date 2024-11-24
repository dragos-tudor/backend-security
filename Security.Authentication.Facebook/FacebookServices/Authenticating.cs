
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Facebook;

partial class FacebookFuncs
{
  public static Task<AuthenticateResult> AuthenticateFacebook(HttpContext context) =>
    AuthenticateOAuth<FacebookOptions>(
      context,
      PostAuthorization,
      ExchangeCodeForTokens,
      AccessFacebookUserInfo,
      ResolveFacebookLogger(context));
}