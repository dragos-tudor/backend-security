
using System.Buffers;

namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  public static string GenerateCorrelationId(ArrayPool<byte>? arrayPool = default) => GenerateRandomString(arrayPool);
}