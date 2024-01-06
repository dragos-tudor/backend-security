namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  static bool IsSessionBasedCookie(ITicketStore ticketStore) => ticketStore is not DefaultTicketStore;
}