
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;

namespace Security.Authentication.BearerToken;

partial class BearerTokenFuncs {

  public static IServiceCollection AddBearerToken(this IServiceCollection services) =>
      AddBearerToken(services, CreateBearerTokenOptions());

  public static IServiceCollection AddBearerToken(
    this IServiceCollection services,
    BearerTokenOptions tokenOptions) =>
      services
        .AddSingleton((services) => tokenOptions)
        .AddSingleton((services) =>
          CreateBearerTokenTicketProtector(ResolveService<IDataProtectionProvider>(services), tokenOptions.SchemeName))
        .AddSingleton((services) =>
          CreateRefreshTokenTicketProtector(ResolveService<IDataProtectionProvider>(services), tokenOptions.SchemeName))
        .AddSingleton(TimeProvider.System);

}