
namespace Security.DataProtection;

partial class DataProtectionFuncs {

  internal static string DecodeToBase64 (byte[] bytes) =>
    Convert.ToBase64String(bytes);

}