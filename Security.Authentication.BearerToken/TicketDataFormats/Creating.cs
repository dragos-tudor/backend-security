
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.DataProtection;

namespace Security.Authentication.BearerToken;

partial class BearerTokenFuncs
{
  const string DateProtectorPurpose = "Microsoft.AspNetCore.Authentication.BearerToken";

  internal static BearerTokenDataFormat CreateBearerTokenDataFormat (
    IDataProtectionProvider dataProtectionProvider,
    string schemeName = BearerTokenDefaults.AuthenticationScheme) =>
      new (CreateDataProtector(dataProtectionProvider, DateProtectorPurpose, schemeName, "BearerToken"));

  internal static RefreshTokenDataFormat CreateRefreshTokenDataFormat (
    IDataProtectionProvider dataProtectionProvider,
    string schemeName = BearerTokenDefaults.AuthenticationScheme) =>
      new (CreateDataProtector(dataProtectionProvider, DateProtectorPurpose, schemeName, "RefreshToken"));
}