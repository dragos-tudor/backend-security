
using Microsoft.AspNetCore.DataProtection;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  const string primaryPurpose = "Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationMiddleware";

  internal static IDataProtector CreateDataProtector (IDataProtectionProvider dataProtectionProvider, string schemeName) =>
    dataProtectionProvider.CreateProtector(primaryPurpose, schemeName, "v2");

}