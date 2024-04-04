
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Facebook;

partial class FacebookFuncs
{
  public static Task<string?> CallbackFacebookEndpoint(HttpContext context, SignInFunc signin) =>
    AuthorizeCallbackOAuth<FacebookOptions>(context, AuthenticateFacebook, signin);
}