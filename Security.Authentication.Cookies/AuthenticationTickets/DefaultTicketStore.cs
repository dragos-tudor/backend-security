
namespace Security.Authentication.Cookies;

public class DefaultTicketStore: ITicketStore
{
  public Task<string> StoreTicket(string ticket) => Task.FromResult(string.Empty);

  public Task<string> UnstoreTicket(string ticketId) => Task.FromResult(string.Empty);
}