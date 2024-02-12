using System.Collections.Generic;
using Microsoft.Extensions.Primitives;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static KeyValuePair<string, string[]> SkipPostAuthorizeParam(KeyValuePair<string, StringValues> pair) =>
    new (pair.Key, pair.Value.ToArray()!);
}