using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;

namespace Security.Authentication.BearerToken;

public sealed class RefreshTokenDataFormat(IDataProtector dataProtector) :
  TicketDataFormat(dataProtector) { }