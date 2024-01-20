
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Security.Testing;

partial class Funcs {

  static Func<string, bool> IsCookieStartingWithName(string cookieName) =>
    cookie => cookie!.StartsWith(cookieName);

}