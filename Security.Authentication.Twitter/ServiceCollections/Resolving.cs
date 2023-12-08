
using Microsoft.Extensions.DependencyInjection;

namespace Security.Authentication.Twitter;

partial class Funcs {

  static T ResolveService<T>(IServiceProvider services) where T: notnull => services.GetRequiredService<T>();

}