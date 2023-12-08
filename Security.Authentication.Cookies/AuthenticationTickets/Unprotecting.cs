
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.Cookies;

partial class Funcs {

  static AuthenticationTicket? UnprotectAuthenticationTicket (string protectedText, ISecureDataFormat<AuthenticationTicket> ticketDataFormat) =>
    ticketDataFormat.Unprotect(protectedText);

}