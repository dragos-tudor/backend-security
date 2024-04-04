
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Security.Sample.Api;

partial class SampleFuncs
{
  static async ValueTask<NoContent> SignOutEndpoint(HttpContext context)
  {
    await SignOutCookie(context, CreateAuthenticationProperties());
    return NoContent();
  }
}