using System.Security.Cryptography;

namespace Security.DataProtection;

partial class DataProtectionFuncs {

  public static byte[] DecryptBytes(SymmetricAlgorithm algorithm, byte[] encryptedBytes, SymmetricEncryptionType encryptionType) =>
    encryptionType switch {
      SymmetricEncryptionType.CBC => algorithm.DecryptCbc(encryptedBytes, algorithm.IV),
      SymmetricEncryptionType.ECB => algorithm.DecryptEcb(encryptedBytes, PaddingMode.None),
      SymmetricEncryptionType.CFB => algorithm.DecryptCfb(encryptedBytes, algorithm.IV),
      _ => algorithm.DecryptEcb(encryptedBytes, PaddingMode.None)
    };

  public static string DecryptString(SymmetricAlgorithm algorithm, string encryptedText, SymmetricEncryptionType encryptionType) =>
    DecodeToBase64(DecryptBytes(algorithm, EncodeFromBase64(encryptedText), encryptionType));

}