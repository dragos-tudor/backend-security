
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  static AuthenticationProperties SetAuthPropsExpiration(AuthenticationProperties authProps, DateTimeOffset currentUtc, TimeSpan? expiresAfter)
  {
    SetAuthPropsIssued(authProps, currentUtc);
    if(expiresAfter is not null) SetAuthPropsExpires(authProps, currentUtc, expiresAfter.Value);
    return authProps;
  }
}