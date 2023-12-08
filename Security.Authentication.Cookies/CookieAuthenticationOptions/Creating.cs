
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;

namespace Security.Authentication.Cookies;

partial class Funcs {

  static CookieAuthenticationOptions CreateCookieAuthenticationOptions (
    ICookieManager cookieManager,
    IDataProtectionProvider dataProtectionProvider,
    string schemeName = CookieAuthenticationDefaults.AuthenticationScheme) =>
      new () {
        AccessDeniedPath = CookieAuthenticationDefaults.AccessDeniedPath,
        ExpireTimeSpan = TimeSpan.FromDays(14),
        LoginPath = CookieAuthenticationDefaults.LoginPath,
        LogoutPath = CookieAuthenticationDefaults.LogoutPath,
        ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter,
        SlidingExpiration = true,
        SchemeName = schemeName,
        CookieManager = cookieManager,
        TicketDataFormat = CreateTicketDataFormat(dataProtectionProvider, schemeName)
      };

}