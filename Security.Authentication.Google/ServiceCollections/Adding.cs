
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;

namespace Security.Authentication.Google;

partial class GoogleFuncs
{
  public static IServiceCollection AddGoogle(
    this IServiceCollection services,
    GoogleOptions googleOptions,
    IDataProtectionProvider? dataProtectionProvider = default) =>
      services
        .AddSingleton(googleOptions)
        .AddSingleton((services) =>
          CreatePropertiesDataFormat(dataProtectionProvider ?? ResolveService<IDataProtectionProvider>(services), googleOptions.SchemeName))
        .AddSingleton(TimeProvider.System);

}