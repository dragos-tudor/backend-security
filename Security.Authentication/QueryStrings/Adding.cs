using System.Collections.Generic;
using Microsoft.AspNetCore.WebUtilities;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static string? AddQueryString(string uri, string? name, string? value) =>
    (name is not null && value is not null)?
      QueryHelpers.AddQueryString(uri, name, value):
      default;

  public static string AddQueryString(string uri, IEnumerable<KeyValuePair<string, string?>> queryString) =>
    QueryHelpers.AddQueryString(uri, queryString);
}