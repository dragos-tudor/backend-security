using System.Security.Claims;

namespace Security.Sample.Endpoints;

partial class EndpointsFuncs
{
  static UserInfoResponse BuildUserInfoResponse (ClaimsPrincipal principal) =>
    new (
      GetPrincipalName(principal)!,
      GetUserSchemeName(principal)!,
      GetUserClaims(principal)
    );
}