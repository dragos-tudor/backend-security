
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static string EncodeBytes (byte[] data) =>
    Base64UrlTextEncoder.Encode(data);
}