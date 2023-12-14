
using System.Linq;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Security.Authorization;

partial class AuthorizationFuncs {

  static AuthenticateResult AuthenticateSchemefullPolicy (
    AuthorizationPolicy policy,
    AuthenticateSchemeFunc authenticateScheme,
    HttpContext context)
  {
    var authenticateResults = AuthenticatePolicySchemes(policy, context, authenticateScheme);
    var expires = MinimumAuthenticationPropertiesExpires(authenticateResults);
    var principals = authenticateResults.Select(result => result.Principal);
    var authenticatedPrincipals = GetAuthenticatedClaimsPrincipals(principals);
    var principal = CombineClaimsPrincipals(authenticatedPrincipals);

    return principal is not null ?
      AuthenticateResult.Success(CreateAuthenticationTicket(principal, JoinPolicySchemeNames(policy), expires)) :
      AuthenticateResult.Success(CreateAuthenticationTicket(CreateClaimsPrincipal()));
  }

  static AuthenticateResult AuthenticateSchemelessPolicy (HttpContext context) =>
    IsAuthenticatedClaimsPrincipal(context.User) ?
      AuthenticateResult.Success(new AuthenticationTicket(context.User, "context.User")) :
      AuthenticateResult.NoResult();


  static AuthenticateResult AuthenticatePolicy (
    AuthorizationPolicy policy,
    AuthenticateSchemeFunc authenticateScheme,
    HttpContext context) =>
      IsSchemelessPolicy(policy!)?
        AuthenticateSchemelessPolicy(context):
        AuthenticateSchemefullPolicy(policy!, authenticateScheme, context);

}