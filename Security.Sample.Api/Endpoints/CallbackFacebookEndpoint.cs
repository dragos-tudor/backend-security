
using System.Threading.Tasks;

namespace Security.Samples;

partial class SampleFuncs
{
  static Task<string?> CallbackFacebookEndpoint(HttpContext context) =>
    CallbackOAuthAsync<FacebookOptions>(
      context,
      AuthenticateFacebookAsync,
      SignInCookie);
}