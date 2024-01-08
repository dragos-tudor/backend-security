
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.DataProtection;

namespace Security.Authentication.BearerToken;

partial class BearerTokenFuncs
{
  internal static TicketDataFormat CreateBearerTokenTicketProtector (
    IDataProtectionProvider dataProtectionProvider,
    string? schemeName = BearerTokenDefaults.AuthenticationScheme) =>
      new (CreateDataProtector(dataProtectionProvider, schemeName!, "BearerToken"));

  internal static TicketDataFormat CreateRefreshTokenTicketProtector (
    IDataProtectionProvider dataProtectionProvider,
    string? schemeName = BearerTokenDefaults.AuthenticationScheme) =>
      new (CreateDataProtector(dataProtectionProvider, schemeName!, "RefreshToken"));

}