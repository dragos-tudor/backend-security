
namespace Security.Authentication.Remote;

partial class RemoteFuncs {

  public static string GenerateCodeVerifier()
  {
    var data = RentBytes(32);
    GenerateRandomBytes(data);

    var codeVerifier = EncodeBytes(data);
    ReturnBytes(data);

    return codeVerifier;
  }

}