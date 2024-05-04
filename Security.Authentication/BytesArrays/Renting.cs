
using System.Buffers;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static byte[] RentBytes (ArrayPool<byte> arrayPool, int length) =>
    arrayPool.Rent(length);
}