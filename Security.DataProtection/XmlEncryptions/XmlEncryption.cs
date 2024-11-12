
namespace Security.DataProtection;
#pragma warning disable CA1024
#pragma warning disable CA1724

public abstract class XmlEncryption {

  static readonly byte[] SecretKey = EncodeFromBase64(GetDataProtectionKey());
  protected static byte[] GetAlgorithmKey() => SecretKey;

  protected static SymmetricAlgorithmType GetAlgorithmType() => SymmetricAlgorithmType.Aes;
  protected static SymmetricEncryptionType GetEncryptionType() => SymmetricEncryptionType.ECB;

}