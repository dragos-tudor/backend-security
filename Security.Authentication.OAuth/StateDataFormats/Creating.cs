
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  public static PropertiesDataFormat CreateStateDataFormat (IDataProtectionProvider dataProtectionProvider, string? schemeName = "OAuth") =>
    new (CreateDataProtector(dataProtectionProvider, typeof(OAuthFuncs).FullName!, schemeName!));

}