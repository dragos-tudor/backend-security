
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization.Policy;

namespace Security.Authorization;

partial class AuthorizationFuncs {

  static bool IsSuccessfulAuthorization (PolicyAuthorizationResult? authzResult) => authzResult is null || authzResult.Succeeded;

  static bool ExistsPrincipal (ClaimsPrincipal? principal) => principal is not null;
}