
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Facebook;

partial class FacebookFuncs
{
  public static Task<string> CallbackFacebookEndpoint(HttpContext context, SignInFunc signin) =>
    CallbackOAuth<FacebookOptions>(context, AuthenticateFacebook, signin, ResolveFacebookLogger(context));
}