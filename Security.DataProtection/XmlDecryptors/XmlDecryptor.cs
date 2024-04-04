
using System.Xml.Linq;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;

namespace Security.DataProtection;

public sealed class XmlDecryptor : XmlEncryption, IXmlDecryptor {

  public XElement Decrypt (XElement encryptedXmlKey) {
    using var algorithm = CreateSymmetricAlgorithm(GetAlgorithmType(), GetAlgorithmKey());
    return DecryptXmlKey(encryptedXmlKey, algorithm, GetEncryptionType());
  }

}

