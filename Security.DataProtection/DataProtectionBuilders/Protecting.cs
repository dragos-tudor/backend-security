using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;

namespace Security.DataProtection;

partial class Funcs {

  public static IDataProtectionBuilder ProtectKeysWithSecret<TEncryptor> (this IDataProtectionBuilder builder, DirectoryInfo keysDirectory) where TEncryptor: IXmlEncryptor, new() =>
    builder
      .PersistKeysToFileSystem(keysDirectory)
      .AddKeyManagementOptions(keyOptions =>
        keyOptions.XmlEncryptor = new TEncryptor());

  public static IDataProtectionBuilder ProtectKeysWithSecret (this IDataProtectionBuilder builder, DirectoryInfo keysDirectory) =>
    builder
    .ProtectKeysWithSecret<XmlEncryptor>(keysDirectory);

}