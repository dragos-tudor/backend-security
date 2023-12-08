
using System.Buffers;

namespace Security.Authentication;

partial class Funcs {

  public static void ReturnBytes (byte[] bytes) =>
    ArrayPool<byte>.Shared.Return(bytes);

}