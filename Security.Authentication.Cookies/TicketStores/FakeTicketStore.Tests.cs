using System.Collections.Generic;
using System.Threading;
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.Cookies;

partial class CookiesTests
{

  internal class FakeTicketStore() : ITicketStore
  {
    readonly Dictionary<string, AuthenticationTicket> Tickets = [];

    static string GenerateTicketId() => Guid.NewGuid().ToString();

    public Task<AuthenticationTicket?> GetTicket(string ticketId, CancellationToken cancellationToken = default) =>
      Tickets.TryGetValue(ticketId, out var ticket)?
        Task.FromResult<AuthenticationTicket?>(ticket):
        Task.FromResult(default(AuthenticationTicket));

    public Task RemoveTicket(string ticketId, CancellationToken cancellationToken = default) =>
      Task.FromResult(Tickets.Remove(ticketId));

    public Task<string> RenewTicket(AuthenticationTicket ticket, string ticketId, CancellationToken cancellationToken = default)
    {
      Tickets[ticketId] = ticket;
      return Task.FromResult(ticketId);
    }

    public Task<string> SetTicket(AuthenticationTicket ticket, CancellationToken cancellationToken = default)
    {
      var ticketId = GenerateTicketId();
      Tickets.Add(ticketId, ticket);
      return Task.FromResult(ticketId);
    }
  }
}