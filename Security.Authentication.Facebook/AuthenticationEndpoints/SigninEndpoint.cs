
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Facebook;

partial class FacebookFuncs
{
  public static Task<string?> SignInFacebookEndpoint(HttpContext context, SignInFunc signin) =>
    SigninOAuthAsync<FacebookOptions>(context, AuthenticateFacebookAsync, signin);
}