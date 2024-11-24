
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Google;

partial class GoogleFuncs
{
  public static Task<AuthenticateResult> AuthenticateGoogle(HttpContext context) =>
    AuthenticateOAuth<GoogleOptions>(
      context,
      PostAuthorization,
      ExchangeCodeForTokens,
      AccessUserInfo,
      ResolveGoogleLogger(context));

}