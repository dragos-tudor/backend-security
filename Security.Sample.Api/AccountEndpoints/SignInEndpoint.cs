using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Security.Sample.Api;

partial class SampleFuncs
{
  static async Task<Results<Ok<UserInfoDto>, BadRequest<string>>> SignInEndpoint(
    HttpContext context,
    CookieAuthenticationOptions authOptions,
    CredentialsDto credentials)
  {
    await SimulateLongProcess(500);
    if (ValidateCredentials(credentials) is string valError)
      return BadRequest(valError);

    var principal = CreatePrincipal(authOptions.SchemeName, [CreateNameClaim(credentials.UserName)]);
    var authTicket = await SignInCookie(context, principal, CreateAuthenticationProperties());
    return Ok(CreateUserInfo(authTicket.Principal));
  }
}