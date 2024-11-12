
using System.Xml.Linq;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;

namespace Security.DataProtection;

public sealed class XmlDecryptor : XmlEncryption, IXmlDecryptor {

  public XElement Decrypt(XElement encryptedElement) {
    using var algorithm = CreateSymmetricAlgorithm(GetAlgorithmType(), GetAlgorithmKey());
    return DecryptXmlKey(encryptedElement, algorithm, GetEncryptionType());
  }

}

