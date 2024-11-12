
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization.Policy;

namespace Security.Authorization;

partial class AuthorizationFuncs
{
  static bool IsAuthorizationChallenged(PolicyAuthorizationResult authzResult) => authzResult.Challenged;

  static bool IsAuthorizationForbidden(PolicyAuthorizationResult authzResult) => authzResult.Forbidden;

  static bool IsAuthorizationSucceeded(PolicyAuthorizationResult authzResult) => authzResult.Succeeded;

  static bool ExistsPrincipal(ClaimsPrincipal? principal) => principal is not null;
}