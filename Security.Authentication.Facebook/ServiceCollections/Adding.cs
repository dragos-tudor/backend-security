using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;

namespace Security.Authentication.Facebook;

partial class FacebookFuncs {

  public static IServiceCollection AddFacebook(
    this IServiceCollection services,
    FacebookOptions facebookOptions,
    IDataProtectionProvider? dataProtectionProvider = default) =>
      services
        .AddSingleton(facebookOptions)
        .AddSingleton((services) =>
          CreatePropertiesDataFormat(dataProtectionProvider ?? ResolveService<IDataProtectionProvider>(services), facebookOptions.SchemeName))
        .AddSingleton(TimeProvider.System);
}