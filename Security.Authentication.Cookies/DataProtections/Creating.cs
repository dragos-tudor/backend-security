
using Microsoft.AspNetCore.DataProtection;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs {

  internal static IDataProtector CreateDataProtector (IDataProtectionProvider dataProtectionProvider, string schemeName) =>
    dataProtectionProvider.CreateProtector("Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationMiddleware", schemeName, "v2");

}