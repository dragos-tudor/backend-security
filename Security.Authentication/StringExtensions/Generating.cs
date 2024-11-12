
using System.Buffers;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static string GenerateRandomString(ArrayPool<byte>? arrayPool = default, int length = 32)
  {
    var sharedArrayPool = ArrayPool<byte>.Shared;
    var bytes = RentBytes(arrayPool ?? sharedArrayPool, length);
    GenerateRandomBytes(bytes);

    var randomValue = EncodeBytes(bytes);
    ReturnBytes(arrayPool ?? sharedArrayPool, bytes);

    return randomValue;
  }
}