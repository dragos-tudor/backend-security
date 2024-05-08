
using Microsoft.Extensions.Configuration;

namespace Security.Sample.App;

partial class AppFuncs
{
  static ResponseCacheSettings GetResponseCacheSettings (ConfigurationManager configuration)
  {
    var responseCache = new ResponseCacheSettings();
    configuration.Bind(responseCache);
    return responseCache;
  }
}