
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Twitter;

partial class TwitterFuncs
{
  public static Task<AuthenticateResult> AuthenticateTwitter(HttpContext context) =>
    AuthenticateOAuth<TwitterOptions>(
      context,
      PostAuthorize,
      ExchangeTwitterCodeForTokens,
      AccessTwitterUserInfo,
      ResolveTwitterLogger(context));
}