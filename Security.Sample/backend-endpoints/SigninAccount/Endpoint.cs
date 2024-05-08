using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Security.Sample.Endpoints;

partial class EndpointsFuncs
{
  public static async Task<Results<Ok, UnauthorizedHttpResult, BadRequest<string>>> SignInAccount(
    HttpContext context,
    CookieAuthenticationOptions authOptions,
    CredentialsRequest credentials)
  {
    await SimulateLongProcess(500);
    if (ValidateCredentials(credentials) is string valError) return BadRequest(valError);
    if (!VerifyCredentials(credentials)) return Unauthorized();

    var principal = CreatePrincipal(authOptions.SchemeName, [CreateNameClaim(credentials.UserName)]);
    await SignInCookie(context, principal, CreateAuthenticationProperties());
    return Ok();
  }
}