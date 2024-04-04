
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs {

  static string ProtectAuthenticationTicket (AuthenticationTicket ticket, TicketDataFormat ticketDataFormat) =>
    ticketDataFormat.Protect(ticket);

}