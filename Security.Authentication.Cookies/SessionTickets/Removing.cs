using System.Threading;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  static Task RemoveSessionTicket(
    ITicketStore ticketStore,
    string ticketId,
    CancellationToken token = default) =>
      ticketStore.RemoveTicket(ticketId, token);

}