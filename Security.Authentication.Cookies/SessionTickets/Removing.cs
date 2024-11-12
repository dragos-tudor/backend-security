using System.Threading;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  static Task RemoveSessionTicket(ITicketStore ticketStore, string ticketId, CancellationToken cancellationToken = default) => ticketStore.RemoveTicket(ticketId, cancellationToken);
}