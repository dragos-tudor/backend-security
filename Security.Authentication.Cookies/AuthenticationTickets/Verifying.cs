
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs {

  static bool AllowAuthenticationTicketRefresh (AuthenticationTicket ticket) => ticket.Properties?.AllowRefresh ?? true;

  static bool AllowAuthenticationTicketSlideExpiration (bool slidingExpiration) => slidingExpiration;

  static bool ExistAuthenticationTicket (AuthenticationTicket? ticket) => ticket is not null;

  static bool IsSetAuthenticationTicketIssued (AuthenticationTicket ticket) => ticket.Properties?.IssuedUtc is not null;

  static bool IsSetAuthenticationTicketExpires (AuthenticationTicket ticket) => ticket.Properties?.ExpiresUtc is not null;

  internal static bool IsExpiredAuthenticationTicket (AuthenticationTicket ticket, DateTimeOffset currentUtc) =>
    IsSetAuthenticationTicketExpires(ticket) &&
    GetAuthenticationTicketExpires(ticket) < currentUtc;

  static bool IsInsideSlindingExpirationInterval (AuthenticationTicket ticket, DateTimeOffset currentUtc) =>
    GetAuthenticationTicketTimeElapsed(ticket, currentUtc) >
    GetAuthenticationTicketTimeRemaining(ticket, currentUtc);

  internal static bool IsRenewableAuthenticationTicket (AuthenticationTicket ticket, DateTimeOffset currentUtc, bool slidingExpiration = true) =>
    AllowAuthenticationTicketSlideExpiration(slidingExpiration) &&
    AllowAuthenticationTicketRefresh(ticket) &&
    IsSetAuthenticationTicketIssued(ticket) &&
    IsSetAuthenticationTicketExpires(ticket) &&
    IsInsideSlindingExpirationInterval(ticket, currentUtc);

}