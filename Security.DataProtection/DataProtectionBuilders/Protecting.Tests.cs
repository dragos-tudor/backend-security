
using System.Security.Cryptography;
using Microsoft.AspNetCore.DataProtection;

namespace Security.DataProtection;

partial class DataProtectionTests {

  static readonly string keysPath = $"{Environment.CurrentDirectory}/keys";
  static readonly DirectoryInfo keysDirectory = new(keysPath);

  static DataProtectionTests () {
    if(!keysDirectory.Exists) keysDirectory.Create();
  }

  [TestMethod]
  public void Data_protectors_with_same_purpose__protect_input__unprotect_output_succeded () {

    var provider = CreateDataProtectionProvider<XmlEncryptor>(keysDirectory);
    var protector1 = provider.CreateProtector("token");
    var protector2 = provider.CreateProtector("token");

    var unencrypted = "some text to encrypt";
    var encrypted = protector1.Protect(unencrypted);

    Assert.AreEqual(protector2.Unprotect(encrypted),  unencrypted);
  }

  [TestMethod]
  public void Data_protectors_providers_with_same_directory_and_same_purpose__protect_input__unprotect_output_succeded () {

    var provider1 = CreateDataProtectionProvider<XmlEncryptor>(keysDirectory);
    var provider2 = CreateDataProtectionProvider<XmlEncryptor>(keysDirectory);
    var protector1 = provider1.CreateProtector("token");
    var protector2 = provider2.CreateProtector("token");

    var unencrypted = "some text to encrypt";
    var encrypted = protector1.Protect(unencrypted);

    Assert.AreEqual(protector2.Unprotect(encrypted),  unencrypted);
  }

  [TestMethod]
  public void Data_protectors_with_different_purposes__protect_input__unprotect_output_failed () {

    var provider = CreateDataProtectionProvider<XmlEncryptor>(keysDirectory);
    var protector1 = provider.CreateProtector("cookie");
    var protector2 = provider.CreateProtector("token");
    var encrypted = protector1.Protect("some text to encrypt");

    var exception = Assert.ThrowsException<CryptographicException>(() => protector2.Unprotect(encrypted));
    StringAssert.Contains(exception.Message, "The payload was invalid.");
  }

}