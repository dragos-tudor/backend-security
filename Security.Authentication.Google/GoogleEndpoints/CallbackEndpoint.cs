
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Google;

partial class GoogleFuncs
{
  public static Task<string?> CallbackGoogleEndpoint(HttpContext context, SignInFunc signIn) =>
    AuthorizeCallbackOAuth<GoogleOptions>(context, AuthenticateGoogle, signIn);
}