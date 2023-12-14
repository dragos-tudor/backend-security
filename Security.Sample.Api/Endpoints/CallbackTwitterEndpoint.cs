
using System.Threading.Tasks;

namespace Security.Samples;

partial class SampleFuncs
{
  static Task<string?> CallbackTwitterEndpoint(HttpContext context) =>
    CallbackOAuthAsync<TwitterOptions>(
      context,
      AuthenticateTwitterAsync,
      SignInCookie);
}