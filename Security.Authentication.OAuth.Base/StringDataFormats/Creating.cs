
using Microsoft.AspNetCore.DataProtection;

namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  public static StringDataFormat CreateStringDataFormat(IDataProtectionProvider dataProtectionProvider, string scheme = "oauth2") =>
    new(new StringSerializer(), CreateDataProtector(dataProtectionProvider, nameof(String), scheme, "v1"));
}