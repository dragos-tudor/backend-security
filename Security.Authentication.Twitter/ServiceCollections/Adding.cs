
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;

namespace Security.Authentication.Twitter;

partial class TwitterFuncs {

  public static IServiceCollection AddTwitter(
    this IServiceCollection services,
    TwitterOptions twitterOptions) =>
      services
        .AddSingleton(twitterOptions)
        .AddSingleton<ISecureDataFormat<AuthenticationProperties>>((services) =>
          CreateStateDataFormat(services.GetRequiredService<IDataProtectionProvider>(), twitterOptions.SchemeName))
        .AddSingleton(TimeProvider.System);

}