
namespace Security.DataProtection;

partial class Funcs {

  public static void SetDataProtectionKey (string secretKey) =>
    Environment.SetEnvironmentVariable(DataProtectionKeyName, secretKey);

}
