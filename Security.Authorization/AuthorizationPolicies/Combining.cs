
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Security.Authorization;

partial class AuthorizationFuncs {

  static Task<AuthorizationPolicy?> CombinePolicies(
    IAuthorizationPolicyProvider policyProvider,
    Endpoint? endpoint) =>
      AuthorizationPolicy.CombineAsync(
        policyProvider,
        GetAllEndpointMetadata<IAuthorizeData>(endpoint) ?? Array.Empty<IAuthorizeData>(),
        GetAllEndpointMetadata<AuthorizationPolicy>(endpoint) ?? Array.Empty<AuthorizationPolicy>()
      );

}