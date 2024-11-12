
using System.Buffers;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static void ReturnBytes(ArrayPool<byte> arrayPool, byte[] data) =>
    arrayPool.Return(data);
}