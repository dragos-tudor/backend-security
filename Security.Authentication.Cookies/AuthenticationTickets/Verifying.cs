
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.Cookies;

partial class Funcs {

  static bool AllowAuthenticationTicketRefresh (AuthenticationTicket ticket) => ticket.Properties.AllowRefresh ?? true;

  static bool AllowAuthenticationTicketSlideExpiration (bool slidingExpiration) => slidingExpiration;

  static bool IsSetAuthenticationTicketIssued (AuthenticationTicket ticket) => ticket.Properties.IssuedUtc is not null;

  static bool IsSetAuthenticationTicketExpires (AuthenticationTicket ticket) => ticket.Properties.ExpiresUtc is not null;

  static bool IsExpiredAuthenticationTicket (AuthenticationTicket ticket, DateTimeOffset currentUtc) =>
    IsSetAuthenticationTicketExpires(ticket) &&
    GetAuthenticationTicketExpires(ticket) < currentUtc;

  static bool IsInsideSlindingExpirationInterval (AuthenticationTicket ticket, DateTimeOffset currentUtc) =>
    GetAuthenticationTicketTimeElapsed(ticket, currentUtc) >
    GetAuthenticationTicketTimeRemaining(ticket, currentUtc);

  static bool IsRenewableAuthenticationTicket (AuthenticationTicket ticket, DateTimeOffset currentUtc, bool slidingExpiration) =>
    AllowAuthenticationTicketSlideExpiration(slidingExpiration) &&
    AllowAuthenticationTicketRefresh(ticket) &&
    IsSetAuthenticationTicketIssued(ticket) &&
    IsSetAuthenticationTicketExpires(ticket) &&
    IsInsideSlindingExpirationInterval(ticket, currentUtc);

}