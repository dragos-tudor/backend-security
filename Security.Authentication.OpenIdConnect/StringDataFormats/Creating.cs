
using Microsoft.AspNetCore.DataProtection;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static StringDataFormat CreateStringDataFormat (IDataProtectionProvider dataProtectionProvider, string name) =>
    new (
      new StringSerializer(),
      CreateDataProtector(dataProtectionProvider, typeof(OpenIdConnectFuncs).FullName!, typeof(string).FullName!, name, "v1")
    );
}