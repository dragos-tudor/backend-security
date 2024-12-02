
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static string? GetAuthenticateResultError (AuthenticateResult authResult) => authResult.Failure?.Message;
}