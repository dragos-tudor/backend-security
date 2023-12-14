
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Security.Authentication.Facebook;

partial class FacebookFuncs {

  static string GenerateAppSecretProof (string appSecret, string accessToken) =>
    HMACSHA256
      .HashData(ToBytes(appSecret), ToBytes(accessToken))
      .Aggregate(new StringBuilder(), AppendHexaStringByte)
      .ToString();

}