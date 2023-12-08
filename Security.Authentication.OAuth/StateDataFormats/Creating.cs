
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;

namespace Security.Authentication.OAuth;

partial class Funcs {

  internal static PropertiesDataFormat CreateStateDataFormat (IDataProtectionProvider dataProtectionProvider, string? schemeName = "OAuth") =>
    new (CreateDataProtector(dataProtectionProvider, typeof(Funcs).FullName!, schemeName!));

}