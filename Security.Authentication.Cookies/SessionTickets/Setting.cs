using System.Threading;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  static Task<string> SetSessionTicket(
    ITicketStore ticketStore,
    AuthenticationTicket ticket,
    CancellationToken token = default) =>
      ticketStore.SetTicket(ticket, GetAuthenticationPropertiesExpires(ticket.Properties), token);

  static void SetSessionTicketId(HttpContext context, string ticketId) =>
    SetAuthenticationFeature(context, new SessionTicketId(ticketId));
}