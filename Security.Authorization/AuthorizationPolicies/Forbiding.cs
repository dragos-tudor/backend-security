
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Security.Authorization;

partial class AuthorizationFuncs {

  static string ForbidPolicy (
    AuthorizationPolicy policy,
    HttpContext context,
    ForbidSchemeFunc forbidScheme) =>
      IsSchemelessPolicy(policy)?
        forbidScheme(context):
        ForbidPolicySchemes(policy, context, forbidScheme).JoinPolicySchemeFailures();
}