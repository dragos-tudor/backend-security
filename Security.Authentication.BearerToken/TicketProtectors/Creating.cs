
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.DataProtection;

namespace Security.Authentication.BearerToken;

partial class BearerTokenFuncs
{
  internal static BearerTokenProtector CreateBearerTokenTicketProtector (
    IDataProtectionProvider dataProtectionProvider,
    string? schemeName = BearerTokenDefaults.AuthenticationScheme) =>
      new (CreateDataProtector(dataProtectionProvider, schemeName!, "BearerToken"));

  internal static RefreshTokenProtector CreateRefreshTokenTicketProtector (
    IDataProtectionProvider dataProtectionProvider,
    string? schemeName = BearerTokenDefaults.AuthenticationScheme) =>
      new (CreateDataProtector(dataProtectionProvider, schemeName!, "RefreshToken"));
}