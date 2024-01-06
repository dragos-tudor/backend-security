using System.Collections.Generic;
using System.Threading;
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.Cookies;

partial class CookiesTests
{

  internal class FakeTicketStore() : ITicketStore
  {
    readonly Dictionary<string, AuthenticationTicket> Tickets = [];

    public Task<AuthenticationTicket?> GetTicket(string ticketId, CancellationToken token = default) =>
      Tickets.TryGetValue(ticketId, out var ticket)?
        Task.FromResult<AuthenticationTicket?>(ticket):
        Task.FromResult(default(AuthenticationTicket));

    public Task RemoveTicket(string ticketId, CancellationToken token = default) =>
      Task.FromResult(Tickets.Remove(ticketId));

    public Task<string> RenewTicket(AuthenticationTicket ticket, string ticketId, CancellationToken token = default)
    {
      Tickets[ticketId] = ticket;
      return Task.FromResult(ticketId);
    }

    public Task<string> SetTicket(AuthenticationTicket ticket, CancellationToken token = default)
    {
      var ticketId = Guid.NewGuid().ToString();
      Tickets.Add(ticketId, ticket);
      return Task.FromResult(ticketId);
    }
  }
}