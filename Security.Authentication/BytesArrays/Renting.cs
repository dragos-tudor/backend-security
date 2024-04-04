
using System.Buffers;

namespace Security.Authentication;

partial class AuthenticationFuncs {

  public static byte[] RentBytes (int length) =>
    ArrayPool<byte>.Shared.Rent(length);

}