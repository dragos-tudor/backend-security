using System.Security.Claims;

namespace Security.Sample.Api;

partial class SampleFuncs
{
  static UserInfoDto CreateUserInfo(ClaimsPrincipal principal) =>
    new (
      GetPrincipalName(principal)!,
      GetUserSchemeName(principal)!,
      GetUserClaims(principal)
    );
}