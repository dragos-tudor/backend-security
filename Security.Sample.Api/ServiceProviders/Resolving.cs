
using Microsoft.Extensions.DependencyInjection;

namespace Security.Samples;

partial class Funcs {

  static T ResolveService<T>(HttpContext context) where T: notnull =>
    context.RequestServices.GetRequiredService<T>();

}