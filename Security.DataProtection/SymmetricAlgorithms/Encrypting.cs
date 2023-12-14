using System.Security.Cryptography;

namespace Security.DataProtection;

partial class DataProtectionFuncs {

  public static byte[] EncryptBytes (SymmetricAlgorithm algorithm, byte[] unencryptedBytes, SymmetricEncryptionType encryptionType) =>
    encryptionType switch {
      SymmetricEncryptionType.CBC => algorithm.EncryptCbc(unencryptedBytes, algorithm.IV),
      SymmetricEncryptionType.ECB => algorithm.EncryptEcb(unencryptedBytes, PaddingMode.None),
      SymmetricEncryptionType.CFB => algorithm.EncryptCfb(unencryptedBytes, algorithm.IV),
      _ => algorithm.DecryptEcb(unencryptedBytes, PaddingMode.None)
    };

  public static string EncryptString (SymmetricAlgorithm algorithm, string unencryptedText, SymmetricEncryptionType encryptionType) =>
    DecodeToBase64(EncryptBytes(algorithm, EncodeFromBase64(unencryptedText), encryptionType));

}