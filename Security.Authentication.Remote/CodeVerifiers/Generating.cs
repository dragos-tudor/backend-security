
namespace Security.Authentication.Remote;

partial class RemoteFuncs {

  public static string GenerateCodeVerifier()
  {
    var bytes = RentBytes(32);
    GenerateRandomBytes(bytes);

    var codeVerifier = EncodeBytes(bytes);
    ReturnBytes(bytes);

    return codeVerifier;
  }

}