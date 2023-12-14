
using Microsoft.AspNetCore.DataProtection;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  internal static IDataProtector CreateDataProtector (IDataProtectionProvider dataProtectionProvider, string purposeType, string schemeName) =>
    dataProtectionProvider.CreateProtector(purposeType, schemeName, "v1");

}