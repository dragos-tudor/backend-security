namespace Security.Authentication.Cookies;

enum AuthenticationTicketState: byte
{
  Regular = 0,
  ExpiredTicket = 1,
  RenewableTicket = 2
}
