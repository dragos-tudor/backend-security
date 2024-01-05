namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  static bool ExistsTicketStore(ITicketStore ticketStore) => ticketStore is not DefaultTicketStore;
}