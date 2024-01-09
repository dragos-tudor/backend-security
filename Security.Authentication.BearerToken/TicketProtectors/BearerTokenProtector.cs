using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;

namespace Security.Authentication.BearerToken;

public sealed class BearerTokenProtector(IDataProtector protector) :
  TicketDataFormat(protector) { }
