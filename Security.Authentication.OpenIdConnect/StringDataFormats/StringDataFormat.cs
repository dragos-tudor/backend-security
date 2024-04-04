using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;

namespace Security.Authentication.OpenIdConnect;

public sealed class StringDataFormat(IDataSerializer<string> dataSerializer, IDataProtector dataProtector):
  SecureDataFormat<string>(dataSerializer, dataProtector)
{ }