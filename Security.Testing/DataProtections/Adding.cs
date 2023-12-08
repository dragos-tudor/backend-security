
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.DataProtection;

namespace Security.Testing;

partial class Funcs {

  public static IServiceCollection AddDataProtection (this IServiceCollection services, string keysDirectory) =>
    services
    .AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(keysDirectory))
    .Services;
}

