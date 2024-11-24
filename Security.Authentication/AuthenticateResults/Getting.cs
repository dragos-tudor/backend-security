
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static string? GetAuthenticateResultFailure (AuthenticateResult authResult) => authResult.Failure?.Message;
}