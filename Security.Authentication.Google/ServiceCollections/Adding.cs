
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;

namespace Security.Authentication.Google;

partial class GoogleFuncs
{
  public static IServiceCollection AddGoogle(
    this IServiceCollection services,
    GoogleOptions googleOptions) =>
      services
        .AddSingleton(googleOptions)
        .AddSingleton<ISecureDataFormat<AuthenticationProperties>>((services) =>
          CreateStateDataFormat(ResolveService<IDataProtectionProvider>(services), googleOptions.SchemeName))
        .AddSingleton(TimeProvider.System);

}