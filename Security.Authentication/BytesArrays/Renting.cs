
using System.Buffers;

namespace Security.Authentication;

partial class Funcs {

  public static byte[] RentBytes (int length) =>
    ArrayPool<byte>.Shared.Rent(length);

}