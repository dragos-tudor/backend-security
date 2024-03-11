
using System.Threading.Tasks;

namespace Security.Sample.Api;

partial class SampleFuncs
{
  static async ValueTask<string> SignOutEndpoint(HttpContext context) =>
    (await SignOutCookie(context, CreateAuthenticationProperties())) ?? string.Empty;
}