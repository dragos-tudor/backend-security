
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Security.Sample.Endpoints;

partial class EndpointsFuncs
{
  public static async ValueTask<NoContent> SignOutAccount(HttpContext context)
  {
    await SignOutCookie(context, CreateAuthenticationProperties());
    return NoContent();
  }
}