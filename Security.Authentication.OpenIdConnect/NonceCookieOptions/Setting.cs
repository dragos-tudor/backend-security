
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  internal static DateTimeOffset? SetNonceCookieOptionsExpires (CookieOptions cookieOptions, DateTimeOffset? expires) =>
    cookieOptions.Expires = expires;
}