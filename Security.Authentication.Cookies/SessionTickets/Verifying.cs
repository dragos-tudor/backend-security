namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  static bool IsSessionBasedTicket(ITicketStore ticketStore) => ticketStore is not DefaultTicketStore;
}