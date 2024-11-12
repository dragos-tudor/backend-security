
using Microsoft.AspNetCore.DataProtection;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static IDataProtector CreateDataProtector(IDataProtectionProvider dataProtectionProvider, string purpose, params string[] subPurposes) => dataProtectionProvider.CreateProtector(purpose, subPurposes);
}