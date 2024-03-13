using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Security.Sample.Api;

partial class SampleFuncs
{
  static async Task<Results<Ok<UserInfoDto>, RedirectHttpResult, BadRequest<string>>> SignInEndpoint(
    HttpContext context,
    CookieAuthenticationOptions authOptions,
    CredentialsDto credentials)
  {
    if (ValidateCredentials(credentials) is string valError)
      return BadRequest(valError);

    var principal = CreatePrincipal(authOptions.SchemeName, [CreateNameClaim(credentials.UserName)]);
    var authTicket = await SignInCookie(context, principal, CreateAuthenticationProperties());
    var location = GetResponseLocation(context.Response);

    return IsNotEmptyUri(location)?
      Redirect(location!):
      Ok(CreateUserInfo(authTicket.Principal));
  }
}