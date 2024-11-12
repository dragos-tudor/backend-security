
using System.Security.Cryptography;
using System.Xml.Linq;

namespace Security.DataProtection;

partial class DataProtectionFuncs {

  static string DecryptXmlKeyValue(XElement encryptedXmlKey, SymmetricAlgorithm algorithm, SymmetricEncryptionType encryptionType) {
    var iv = GetAlgorithmIV(encryptedXmlKey);
    var algorithmWithIV = SetAlgorithmIV(algorithm, iv);
    return DecryptString(algorithmWithIV, encryptedXmlKey.Value, encryptionType);
  }

  static string GetAlgorithmIV(XElement encryptedXmlKey) =>
   (string)encryptedXmlKey.Attribute(KeyIVName)!;

  static SymmetricAlgorithm SetAlgorithmIV(SymmetricAlgorithm algorithm, string iv) {
    algorithm.IV = EncodeFromBase64(iv);
    return algorithm;
  }

  public static XElement DecryptXmlKey(XElement encryptedXmlKey, SymmetricAlgorithm algorithm, SymmetricEncryptionType encryptionType) =>
    new(encryptedXmlKey) {
      Value = DecryptXmlKeyValue(encryptedXmlKey, algorithm, encryptionType)
    };

}