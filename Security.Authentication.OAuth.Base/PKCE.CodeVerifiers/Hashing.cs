
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.WebUtilities;

namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  public static string HashCodeVerifier(string codeVerifier)
  {
    var codeVerifierRaw = Encoding.UTF8.GetBytes(codeVerifier);
    var codeChallengeRaw = SHA256.HashData(codeVerifierRaw);
    var codeChallenge = WebEncoders.Base64UrlEncode(codeChallengeRaw);
    return codeChallenge;
  }
}