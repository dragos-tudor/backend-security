
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace Security.Authorization;

partial class AuthorizationFuncs
{
  static AuthenticationTicket CreateAuthenticationTicket (ClaimsPrincipal principal, string schemeName, DateTimeOffset? expiresUtc = default) =>
    new (principal, new AuthenticationProperties() { ExpiresUtc = expiresUtc }, schemeName);
}