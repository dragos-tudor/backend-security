
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  internal static TicketDataFormat CreateTicketDataFormat (
    IDataProtectionProvider dataProtectionProvider,
    string? schemeName = CookieAuthenticationDefaults.AuthenticationScheme) =>
      new (CreateDataProtector(dataProtectionProvider, schemeName!));

}