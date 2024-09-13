
using Microsoft.AspNetCore.Http;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static string BuildChallengePath (AuthenticationOptions authOptions, string returnUri) => $"{authOptions.LoginPath}{QueryString.Create(authOptions.ReturnUrlParameter, returnUri)}";

  public static string BuildForbidPath (AuthenticationOptions authOptions, string returnUri) => $"{authOptions.AccessDeniedPath}{QueryString.Create(authOptions.ReturnUrlParameter, returnUri)}";
}