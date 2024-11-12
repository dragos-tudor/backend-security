
using System.Threading;
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  static Task<AuthenticationTicket?> GetSessionTicket(ITicketStore ticketStore, string ticketId, CancellationToken cancellationToken = default) => ticketStore.GetTicket(ticketId, cancellationToken);
}