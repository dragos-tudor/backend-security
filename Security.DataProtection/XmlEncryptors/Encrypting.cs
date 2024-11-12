
using System.Security.Cryptography;
using System.Xml.Linq;

namespace Security.DataProtection;

partial class DataProtectionFuncs {

  internal const string KeyIVName = "iv";

  static XAttribute GetXmlAttributeIV(SymmetricAlgorithm algorithm) =>
    new(KeyIVName, DecodeToBase64(algorithm.IV));

  public static XElement EncryptXmlKey(XElement unencryptedXmlKey, SymmetricAlgorithm algorithm, SymmetricEncryptionType encryptionType) =>
    new(unencryptedXmlKey.Name, GetXmlAttributeIV(algorithm)) {
      Value = EncryptString(algorithm, unencryptedXmlKey.Value, encryptionType)
    };

}