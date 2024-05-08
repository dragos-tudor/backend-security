
using Microsoft.Extensions.Logging;

namespace Security.Sample.Api;

partial class ApiFuncs
{
  static ILoggerFactory ResolveLoggerFactory(IServiceProvider services) =>
    ResolveService<ILoggerFactory>(services);
}