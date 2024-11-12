
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  static string ProtectAuthenticationTicket(AuthenticationTicket ticket, TicketDataFormat ticketDataProtector) => ticketDataProtector.Protect(ticket);
}