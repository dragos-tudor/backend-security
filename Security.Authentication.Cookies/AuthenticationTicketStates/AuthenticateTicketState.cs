namespace Security.Authentication.Cookies;

enum AuthenticationTicketState: byte
{
  Valid = 0,
  Expired = 1,
  Renewable = 2
}
