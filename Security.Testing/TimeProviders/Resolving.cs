using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Time.Testing;

namespace Security.Testing;

partial class Funcs
{
  public static FakeTimeProvider ResolveFakeTimeProvider(IServiceProvider services) =>
    (FakeTimeProvider)services.GetRequiredService<TimeProvider>();
}