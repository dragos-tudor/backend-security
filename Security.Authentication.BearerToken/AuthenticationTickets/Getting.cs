using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.BearerToken;

partial class BearerTokenFuncs
{
  static DateTimeOffset? GetAuthenticationTicketExpires(AuthenticationTicket ticket) => ticket.Properties?.ExpiresUtc;
}