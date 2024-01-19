
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;

namespace Security.Authentication.Twitter;

partial class TwitterFuncs {

  public static IServiceCollection AddTwitter(
    this IServiceCollection services,
    TwitterOptions twitterOptions,
    IDataProtectionProvider? dataProtectionProvider = default) =>
      services
        .AddSingleton(twitterOptions)
        .AddSingleton((services) =>
          CreatePropertiesDataFormat(dataProtectionProvider ?? ResolveService<IDataProtectionProvider>(services), twitterOptions.SchemeName))
        .AddSingleton(TimeProvider.System);

}