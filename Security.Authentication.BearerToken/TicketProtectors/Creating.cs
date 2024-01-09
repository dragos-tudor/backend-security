
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.DataProtection;

namespace Security.Authentication.BearerToken;

partial class BearerTokenFuncs
{
  internal static ISecureDataFormat<AuthenticationTicket> CreateBearerTokenTicketProtector (
    IDataProtectionProvider dataProtectionProvider,
    string? schemeName = BearerTokenDefaults.AuthenticationScheme) =>
      new TicketDataFormat(CreateDataProtector(dataProtectionProvider, schemeName!, "BearerToken"));

  internal static ISecureDataFormat<AuthenticationTicket> CreateRefreshTokenTicketProtector (
    IDataProtectionProvider dataProtectionProvider,
    string? schemeName = BearerTokenDefaults.AuthenticationScheme) =>
      new TicketDataFormat(CreateDataProtector(dataProtectionProvider, schemeName!, "RefreshToken"));
}