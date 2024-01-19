
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.WebUtilities;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  public static string HashCodeVerifier (string codeVerifier) {
    var codeVerifierBytes = Encoding.UTF8.GetBytes(codeVerifier);
    var codeChallengeBytes = SHA256.HashData(codeVerifierBytes);
    var codeChallenge = WebEncoders.Base64UrlEncode(codeChallengeBytes);
    return codeChallenge;
  }

}