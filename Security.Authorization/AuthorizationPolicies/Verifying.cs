
using Microsoft.AspNetCore.Authorization;

namespace Security.Authorization;

partial class AuthorizationFuncs {

  static bool IsSchemelessPolicy (AuthorizationPolicy policy) =>
    policy.AuthenticationSchemes is null ||
    policy.AuthenticationSchemes.Count == 0;

}