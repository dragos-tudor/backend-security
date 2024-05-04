
using System.Buffers;

namespace Security.Authentication.Remote;

partial class RemoteFuncs
{
  public static string GenerateCorrelationId (ArrayPool<byte>? arrayPool = default)
  {
    var sharedArrayPool = ArrayPool<byte>.Shared;
    var bytes = RentBytes(arrayPool ?? sharedArrayPool, 32);
    GenerateRandomBytes(bytes);

    var correlationId = EncodeBytes(bytes);
    ReturnBytes(arrayPool ?? sharedArrayPool, bytes);

    return correlationId;
  }
}