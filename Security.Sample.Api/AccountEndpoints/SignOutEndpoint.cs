
using System.Threading.Tasks;

namespace Security.Samples;

partial class SampleFuncs
{
  static ValueTask<string?> SignOutEndpoint(HttpContext context) =>
    SignOutCookie(context, CreateAuthenticationProperties());
}