
using static Microsoft.AspNetCore.Authentication.AuthenticateResult;

namespace Security.Samples;

partial class Funcs {

  static AuthenticateResult AuthenticateScheme (HttpContext context, string schemeName) =>
    IsClaimsPrincipalWithScheme(GetContextUser(context), schemeName)?
      Success(new AuthenticationTicket(GetContextUser(context)!, schemeName)):
      NoResult();

}