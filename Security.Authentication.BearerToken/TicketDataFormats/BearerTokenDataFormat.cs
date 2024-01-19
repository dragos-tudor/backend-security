using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;

namespace Security.Authentication.BearerToken;

public sealed class BearerTokenDataFormat(IDataProtector dataProtector) :
  TicketDataFormat(dataProtector) { }
