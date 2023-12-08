
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Security.Authorization;

partial class Funcs {

  static string ForbidPolicy (
    AuthorizationPolicy policy,
    HttpContext context,
    ForbidSchemeFunc forbidSchemeFunc) =>
      IsSchemelessPolicy(policy)?
        forbidSchemeFunc(context):
        ForbidPolicySchemes(policy, context, forbidSchemeFunc).JoinPolicySchemeFailures();
}