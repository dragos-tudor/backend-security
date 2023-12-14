using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;

namespace Security.Authentication.Facebook;

partial class FacebookFuncs {

  public static IServiceCollection AddFacebook(
    this IServiceCollection services,
    FacebookOptions facebookOptions) =>
      services
        .AddSingleton(facebookOptions)
        .AddSingleton<ISecureDataFormat<AuthenticationProperties>>((services) =>
          CreateStateDataFormat(services.GetRequiredService<IDataProtectionProvider>(), facebookOptions.SchemeName))
        .AddSingleton(TimeProvider.System);

}