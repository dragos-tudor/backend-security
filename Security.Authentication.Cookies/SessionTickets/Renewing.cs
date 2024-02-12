using System.Threading;
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  static Task<string> RenewSessionTicket(
    ITicketStore ticketStore,
    AuthenticationTicket ticket,
    string ticketId,
    CancellationToken cancellationToken = default) =>
      ticketStore.RenewTicket(ticket, ticketId, cancellationToken);
}