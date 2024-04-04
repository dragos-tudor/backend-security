using System.Security.Cryptography;

namespace Security.DataProtection;

partial class DataProtectionFuncs {

  static SymmetricAlgorithm CreateAlgorithm (SymmetricAlgorithmType algorithmType) =>
    algorithmType switch {
      SymmetricAlgorithmType.Aes => Aes.Create(),
      SymmetricAlgorithmType.RC2 => RC2.Create(),
      SymmetricAlgorithmType.TripleDES => TripleDES.Create(),
      _ => Aes.Create()
    };

  public static SymmetricAlgorithm CreateSymmetricAlgorithm (SymmetricAlgorithmType algorithmType, byte[] key) {
    var algorithm = CreateAlgorithm(algorithmType);
    algorithm.Key = key;
    return algorithm;
  }

}