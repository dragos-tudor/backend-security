
using System.Threading;
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.Cookies;

public interface ITicketStore
{
  Task<AuthenticationTicket?> GetTicket(string ticketId, CancellationToken cancellationToken = default);

  Task RemoveTicket(string ticketId, CancellationToken cancellationToken = default);

  Task<string> RenewTicket(AuthenticationTicket ticket, string ticketId, CancellationToken cancellationToken = default);

  Task<string> SetTicket(AuthenticationTicket ticket, CancellationToken cancellationToken = default);
}