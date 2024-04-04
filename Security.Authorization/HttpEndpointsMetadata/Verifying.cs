
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Security.Authorization;

partial class AuthorizationFuncs {

  const string SuppressUseHttpContextAsAuthorizationResource = "Microsoft.AspNetCore.Authorization.SuppressUseHttpContextAsAuthorizationResource";

  static bool IsAnonymousEndpoint (Endpoint? endpoint) =>
    GetEndpointMetadata<IAllowAnonymous>(endpoint) is not null;

  static bool IsEndpointResource () =>
    AppContext.TryGetSwitch(SuppressUseHttpContextAsAuthorizationResource, out var useEndpointAsResource) &&
    useEndpointAsResource;

}