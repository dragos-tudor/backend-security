
using Microsoft.AspNetCore.DataProtection;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  const string CookiePurpose = "Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationMiddleware";

  static IDataProtector CreateDataProtector (IDataProtectionProvider dataProtectionProvider, string schemeName) =>
    dataProtectionProvider.CreateProtector(CookiePurpose, schemeName, "v2");
}