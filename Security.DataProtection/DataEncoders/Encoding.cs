
namespace Security.DataProtection;

partial class DataProtectionFuncs {

  internal static byte[] EncodeFromBase64(string text) =>
    Convert.FromBase64String(text);

}