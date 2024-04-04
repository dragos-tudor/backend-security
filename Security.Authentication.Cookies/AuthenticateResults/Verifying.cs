using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  static bool IsExpiredAuthenticationTicket (AuthenticateResult result) => result.Failure?.Message == TicketExpired;
  static bool IsRenewedAuthenticationTicket (AuthenticateResult result, DateTimeOffset currentUtc) => result.Properties?.IssuedUtc == currentUtc;
}