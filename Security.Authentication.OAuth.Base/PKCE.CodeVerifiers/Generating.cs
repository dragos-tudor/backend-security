
using System.Buffers;

namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  public static string GenerateCodeVerifier(ArrayPool<byte>? arrayPool = default) => GenerateRandomString(arrayPool);
}