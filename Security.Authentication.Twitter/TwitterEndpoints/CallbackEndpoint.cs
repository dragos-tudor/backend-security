
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Twitter;

partial class TwitterFuncs
{
  public static Task<string?> CallbackTwitterEndpoint(HttpContext context, SignInFunc signIn) =>
    CallbackOAuth<TwitterOptions>(context, AuthenticateTwitter, signIn);
}