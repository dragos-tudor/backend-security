
namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  static string GenerateCodeVerifier()
  {
    var bytes = RentBytes(32);
    GenerateRandomBytes(bytes);

    var codeVerifier = EncodeBytes(bytes);
    ReturnBytes(bytes);

    return codeVerifier;
  }

}