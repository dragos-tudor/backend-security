using System.Threading.Tasks;

namespace Security.Sample.Api;

partial class SampleFuncs
{
  static async Task<UserInfoDto?> SignInEndpoint(
    HttpContext context,
    CookieAuthenticationOptions authOptions,
    CredentialsDto credentials)
  {
    if (!CheckCredentials(credentials))
      return default;

    var principal = CreatePrincipal(authOptions.SchemeName, [CreateNameClaim(credentials.UserName)]);
    var authTicket = await SignInCookie(context, principal, CreateAuthenticationProperties());

    return CreateUserInfo(authTicket.Principal);
  }

}