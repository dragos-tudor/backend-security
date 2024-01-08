
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.DataProtection;

namespace Security.Authentication.BearerToken;

partial class BearerTokenFuncs
{
  const string primaryPurpose = "Microsoft.AspNetCore.Authentication.BearerToken";

  internal static IDataProtector CreateDataProtector (
    IDataProtectionProvider dataProtectionProvider,
    string tokenType,
    string schemeName = BearerTokenDefaults.AuthenticationScheme) =>
      dataProtectionProvider.CreateProtector(primaryPurpose, schemeName, tokenType);

}