
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  static string ToAuthenticateResultString(AuthenticateResult authResult) =>
    authResult switch {
      var result when result.Succeeded => $"authentication succedded: {GetPrincipalNameId(authResult.Principal)}",
      var result when result.None => "no authentication",
      _ => $"authentication failure: {GetAuthenticateResultError(authResult)}"
    };

  public static Task<AuthenticateResult> ToTask(this AuthenticateResult authResult) => Task.FromResult(authResult);
}