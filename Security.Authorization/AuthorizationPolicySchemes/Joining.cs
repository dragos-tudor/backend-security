
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Security.Authorization;

partial class AuthorizationFuncs {

  static string JoinPolicySchemeNames (AuthorizationPolicy policy, string separator = ";") =>
    string.Join(separator, policy.AuthenticationSchemes);

  static string JoinPolicySchemeFailures (this IEnumerable<string> failures, string separator = ",") =>
    string.Join(separator, failures);

}