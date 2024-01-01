
namespace Security.Authentication.Cookies;

public interface ITicketStore
{
  Task<string> StoreTicket(string ticket);

  Task<string> UnstoreTicket(string ticketId);
}