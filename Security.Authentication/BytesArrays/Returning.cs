
using System.Buffers;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static void ReturnBytes (byte[] data) =>
    ArrayPool<byte>.Shared.Return(data);
}