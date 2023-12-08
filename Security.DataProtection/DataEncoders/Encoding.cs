
namespace Security.DataProtection;

partial class Funcs {

  internal static byte[] EncodeFromBase64 (string text) =>
    Convert.FromBase64String(text);

}