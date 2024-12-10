
using System.Security.Cryptography;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  const string InvalidAlghoritmMoniker = "The algorithm '{0}' could not be used to create a '{1}'. error {2}.";

  static (HashAlgorithm?, string?) CreateHashAlgorithm(
    OpenIdConnectValidationOptions validationOptions,
    string algorithm)
  {
    try
    {
      var hashAlgorithmMap = validationOptions.HashAlgorithmMap;
      var cryptoProviderFactory = validationOptions.CryptoProviderFactory;

      if (hashAlgorithmMap.TryGetValue(algorithm, out string? hashAlgorithm))
        return (cryptoProviderFactory.CreateHashAlgorithm(hashAlgorithm), default);

      return (cryptoProviderFactory.CreateHashAlgorithm(algorithm), default);
    }
    catch (Exception ex)
    {
      return (default, InvalidAlghoritmMoniker.Format(algorithm, typeof(HashAlgorithm), ex));
    }
  }
}