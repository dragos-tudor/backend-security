
using Microsoft.AspNetCore.DataProtection;

namespace Security.Authentication.Remote;

partial class RemoteFuncs
{
  public static IDataProtector CreateDataProtector (
    IDataProtectionProvider dataProtectionProvider,
    string purpose,
    params string[] subPurposes) =>
      dataProtectionProvider.CreateProtector(purpose, subPurposes);
}