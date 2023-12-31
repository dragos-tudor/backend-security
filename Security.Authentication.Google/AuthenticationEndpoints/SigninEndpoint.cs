
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Google;

partial class GoogleFuncs
{
  public static Task<string?> SignInGoogleEndpoint(HttpContext context, SignInFunc signIn) =>
    SigninOAuthAsync<GoogleOptions>(context, AuthenticateGoogleAsync, signIn);
}