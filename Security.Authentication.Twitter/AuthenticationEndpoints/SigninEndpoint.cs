
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Twitter;

partial class TwitterFuncs
{
  public static Task<string?> SignInTwitterEndpoint(HttpContext context, SignInFunc signIn) =>
    SigninOAuthAsync<TwitterOptions>(context, AuthenticateTwitterAsync, signIn);
}