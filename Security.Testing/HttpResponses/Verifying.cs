
namespace Security.Testing;

partial class Funcs
{
  static Func<string, bool> IsCookieStartingWithName(string cookieName) => cookie => cookie!.StartsWith(cookieName, StringComparison.Ordinal);
}