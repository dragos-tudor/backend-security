
using Microsoft.AspNetCore.DataProtection;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs {

  internal static IDataProtector CreateDataProtector (IDataProtectionProvider dataProtectionProvider, string purpose, params string[] subPurposes) =>
    dataProtectionProvider.CreateProtector(purpose, subPurposes);

}