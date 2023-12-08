using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;

namespace Security.DataProtection;

partial class Funcs {

  public static IDataProtectionProvider CreateDataProtectionProvider () =>
    CreateDataProtectionProvider(new DirectoryInfo(Environment.CurrentDirectory));

  public static IDataProtectionProvider CreateDataProtectionProvider<TEncryptor> (DirectoryInfo keysDirectory) where TEncryptor: IXmlEncryptor, new() =>
    DataProtectionProvider.Create(keysDirectory, (builder) =>
      builder.ProtectKeysWithSecret<TEncryptor>(keysDirectory));

  public static IDataProtectionProvider CreateDataProtectionProvider (DirectoryInfo keysDirectory) =>
    DataProtectionProvider.Create(keysDirectory, (builder) =>
      builder.ProtectKeysWithSecret(keysDirectory));

}