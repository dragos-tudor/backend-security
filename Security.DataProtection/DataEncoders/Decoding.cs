
namespace Security.DataProtection;

partial class Funcs {

  internal static string DecodeToBase64 (byte[] bytes) =>
    Convert.ToBase64String(bytes);

}