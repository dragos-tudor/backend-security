
using Microsoft.IdentityModel.Tokens;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  const string DifferentHashes = "The expected hash '{0}' different than current text '{1}' hask, algorithm: '{2}'.";

  static string? ValidateHash(
    OpenIdConnectValidationOptions validationOptions,
    string expectedHash,
    string currentText,
    string algorithm)
  {
    var (hashAlgorithm, error) = CreateHashAlgorithm(validationOptions, algorithm);
    if (error is not null) return error;

    var currentBytes = hashAlgorithm!.ComputeHash(Encoding.ASCII.GetBytes(currentText));
    var currentHash = Base64UrlEncoder.Encode(currentBytes, 0, currentBytes.Length / 2);
    hashAlgorithm.Dispose();

    if (!string.Equals(expectedHash, currentHash))
      return FormatString(DifferentHashes, expectedHash, currentText, algorithm);

    return default;
  }
}