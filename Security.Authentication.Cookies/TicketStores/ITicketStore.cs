
using System.Threading;
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.Cookies;

public interface ITicketStore
{
  Task<AuthenticationTicket?> GetTicket(string ticketId, CancellationToken token = default);

  Task RemoveTicket(string ticketId, CancellationToken token = default);

  Task<string> RenewTicket(AuthenticationTicket ticket, string ticketId, CancellationToken token = default);

  Task<string> SetTicket(AuthenticationTicket ticket, CancellationToken token = default);
}