
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.Cookies;

partial class Funcs {

  static string ProtectAuthenticationTicket (AuthenticationTicket ticket, ISecureDataFormat<AuthenticationTicket> ticketDataFormat) =>
    ticketDataFormat.Protect(ticket);

}