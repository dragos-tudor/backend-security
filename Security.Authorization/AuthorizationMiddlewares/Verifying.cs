
using Microsoft.AspNetCore.Authorization.Policy;

namespace Security.Authorization;

partial class AuthorizationFuncs {

  static bool IsSuccessfulAuthorization (PolicyAuthorizationResult? authorizationResult) =>
    authorizationResult is null || authorizationResult.Succeeded;

}