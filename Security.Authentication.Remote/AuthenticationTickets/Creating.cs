
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.Remote;

partial class RemoteFuncs
{
  public static AuthenticationTicket CreateAuthenticationTicket (ClaimsPrincipal principal, AuthenticationProperties? authProperties, string schemeName) =>
    new (principal, authProperties, schemeName);
}