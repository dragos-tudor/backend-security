using Microsoft.Extensions.DependencyInjection;

namespace Security.Sample.App;

partial class AppFuncs
{
  static IServiceCollection AddServices(IServiceCollection services) =>
    services.AddLogging();
}