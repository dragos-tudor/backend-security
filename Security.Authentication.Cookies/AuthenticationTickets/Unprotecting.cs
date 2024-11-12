
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  static AuthenticationTicket? UnprotectAuthenticationTicket(string protectedText, TicketDataFormat ticketDataProtector) => ticketDataProtector.Unprotect(protectedText);
}