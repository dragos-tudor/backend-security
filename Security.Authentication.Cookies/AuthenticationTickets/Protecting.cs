
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs {

  static string ProtectAuthenticationTicket (AuthenticationTicket ticket, ISecureDataFormat<AuthenticationTicket> ticketProtector) =>
    ticketProtector.Protect(ticket);

}