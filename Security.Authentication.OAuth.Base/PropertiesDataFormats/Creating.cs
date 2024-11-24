
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;

namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  public static PropertiesDataFormat CreatePropertiesDataFormat(IDataProtectionProvider dataProtectionProvider, string scheme = "oauth2") =>
    new(CreateDataProtector(dataProtectionProvider, nameof(AuthenticationProperties), scheme, "v1"));
}