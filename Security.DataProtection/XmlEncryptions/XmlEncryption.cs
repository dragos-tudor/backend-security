
#pragma warning disable CA1822

namespace Security.DataProtection;

public abstract class XmlEncryption {

  static readonly byte[] SecretKey = default!;
  static XmlEncryption() =>
    SecretKey = EncodeFromBase64(GetDataProtectionKey());

  protected byte[] GetAlgorithmKey () => SecretKey;
  protected SymmetricAlgorithmType GetAlgorithmType () => SymmetricAlgorithmType.Aes;
  protected SymmetricEncryptionType GetEncryptionType() => SymmetricEncryptionType.ECB;

}