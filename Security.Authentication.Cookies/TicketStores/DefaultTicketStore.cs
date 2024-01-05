
using System.Threading;
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.Cookies;

public class DefaultTicketStore: ITicketStore
{
  public Task<AuthenticationTicket?> GetTicket(string ticketId, CancellationToken token = default) =>
    Task.FromResult(default(AuthenticationTicket));

  public Task RemoveTicket(string ticketId, CancellationToken token = default) =>
    Task.CompletedTask;

  public Task<string> RenewTicket(AuthenticationTicket ticket, string ticketId, DateTimeOffset? expiresAt, CancellationToken token = default) =>
    Task.FromResult(ticketId);

  public Task<string> SetTicket(AuthenticationTicket ticket, DateTimeOffset? expiresAt, CancellationToken token = default) =>
    Task.FromResult(string.Empty);
}