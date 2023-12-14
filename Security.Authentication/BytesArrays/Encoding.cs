
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication;

partial class AuthenticationFuncs {

  public static string EncodeBytes (byte[] bytes) =>
    Base64UrlTextEncoder.Encode(bytes);

}