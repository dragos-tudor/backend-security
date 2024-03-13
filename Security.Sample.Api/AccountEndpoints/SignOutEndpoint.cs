
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Security.Sample.Api;

partial class SampleFuncs
{
  static async ValueTask<Results<Ok<string>, RedirectHttpResult>> SignOutEndpoint(HttpContext context)
  {
    var location = await SignOutCookie(context, CreateAuthenticationProperties());
    return IsNotEmptyUri(location)?
      Redirect(location!):
      Ok(string.Empty);
  }
}