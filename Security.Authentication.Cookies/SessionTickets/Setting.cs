using System.Threading;
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  static Task<string> SetSessionTicket(ITicketStore ticketStore, AuthenticationTicket ticket, CancellationToken cancellationToken = default) => ticketStore.SetTicket(ticket, cancellationToken);
}