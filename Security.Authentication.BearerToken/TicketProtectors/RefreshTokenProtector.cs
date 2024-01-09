using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;

namespace Security.Authentication.BearerToken;

public sealed class RefreshTokenProtector(IDataProtector protector) :
  TicketDataFormat(protector) { }