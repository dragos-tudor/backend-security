
using System.Xml.Linq;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;

namespace Security.DataProtection;

public sealed class XmlEncryptor : XmlEncryption, IXmlEncryptor {

  public EncryptedXmlInfo Encrypt (XElement unencryptedXmlKey) {
    using var algorithm = CreateSymmetricAlgorithm(GetAlgorithmType(), GetAlgorithmKey());
    return new (EncryptXmlKey(unencryptedXmlKey, algorithm, GetEncryptionType()), typeof(XmlDecryptor));
  }

}

