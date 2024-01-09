
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;

namespace Security.Authentication.BearerToken;

partial class BearerTokenFuncs {

  public static IServiceCollection AddBearerToken(this IServiceCollection services) =>
      AddBearerToken(services, CreateBearerTokenOptions());

  public static IServiceCollection AddBearerToken(
    this IServiceCollection services,
    BearerTokenOptions authOptions) =>
      services
        .AddSingleton((services) => authOptions)
        .AddSingleton((services) => (IBearerTokenProtector)
          CreateBearerTokenTicketProtector(services.GetRequiredService<IDataProtectionProvider>(), authOptions.SchemeName))
        .AddSingleton((services) => (IRefreshTokenProtector)
          CreateRefreshTokenTicketProtector(services.GetRequiredService<IDataProtectionProvider>(), authOptions.SchemeName))
        .AddSingleton(TimeProvider.System);

}