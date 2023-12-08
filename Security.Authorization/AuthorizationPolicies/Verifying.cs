
using Microsoft.AspNetCore.Authorization;

namespace Security.Authorization;

partial class Funcs {

  static bool IsSchemelessPolicy (AuthorizationPolicy policy) =>
    policy.AuthenticationSchemes is null ||
    policy.AuthenticationSchemes.Count == 0;

}