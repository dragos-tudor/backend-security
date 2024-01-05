using System.Threading;
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  static Task<string> RenewSessionTicket(
    ITicketStore ticketStore,
    AuthenticationTicket ticket,
    string ticketId,
    CancellationToken token = default) =>
      ticketStore.RenewTicket(ticket, ticketId, GetAuthenticationPropertiesExpires(ticket.Properties), token);
}