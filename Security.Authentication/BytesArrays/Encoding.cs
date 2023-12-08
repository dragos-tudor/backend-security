
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication;

partial class Funcs {

  public static string EncodeBytes (byte[] bytes) =>
    Base64UrlTextEncoder.Encode(bytes);

}