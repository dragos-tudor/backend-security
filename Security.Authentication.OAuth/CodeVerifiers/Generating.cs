
namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  public static string GenerateCodeVerifier()
  {
    var bytes = RentBytes(32);
    GenerateRandomBytes(bytes);

    var codeVerifier = EncodeBytes(bytes);
    ReturnBytes(bytes);

    return codeVerifier;
  }

}