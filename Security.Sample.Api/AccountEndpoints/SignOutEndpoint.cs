
using System.Threading.Tasks;

namespace Security.Sample.Api;

partial class SampleFuncs
{
  static ValueTask<string?> SignOutEndpoint(HttpContext context) =>
    SignOutCookie(context, CreateAuthenticationProperties());
}