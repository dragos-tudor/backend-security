
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  public static PropertiesDataFormat CreatePropertiesDataFormat (IDataProtectionProvider dataProtectionProvider, string name = "OAuth") =>
    new (CreateDataProtector(dataProtectionProvider, typeof(OAuthFuncs).FullName!, name, "v1"));
}