
namespace Security.DataProtection;

partial class DataProtectionFuncs {

  const string DataProtectionKeyName = "DATA_PROTECTION_KEY";

  internal static string GetDataProtectionKey() =>
    Environment.GetEnvironmentVariable(DataProtectionKeyName) ?? throw new ArgumentException($"Missing environment secret key [{DataProtectionKeyName}].");

}
