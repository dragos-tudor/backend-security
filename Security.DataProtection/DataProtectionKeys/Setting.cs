
namespace Security.DataProtection;

partial class DataProtectionFuncs {

  public static void SetDataProtectionKey (string secretKey) =>
    Environment.SetEnvironmentVariable(DataProtectionKeyName, secretKey);

}
