using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  static bool IsExpiredResultTicket (AuthenticateResult result) => result.Failure?.Message == TicketExpired;
  static bool IsRenewableResultTicket (AuthenticateResult result, DateTimeOffset currentUtc) => result.Properties?.IssuedUtc == currentUtc;
}