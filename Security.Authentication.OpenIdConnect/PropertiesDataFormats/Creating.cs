
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static PropertiesDataFormat CreatePropertiesDataFormat (IDataProtectionProvider dataProtectionProvider, string name) =>
    new (CreateDataProtector(dataProtectionProvider, typeof(OpenIdConnectFuncs).FullName!, name, "v1"));
}