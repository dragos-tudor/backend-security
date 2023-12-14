
using System.Threading.Tasks;

namespace Security.Samples;

partial class SampleFuncs
{
  static Task<string?> CallbackGoogleEndpoint(HttpContext context) =>
    CallbackOAuthAsync<GoogleOptions>(
      context,
      AuthenticateGoogleAsync,
      SignInCookie);
}