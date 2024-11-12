using System.Collections.Generic;
using Microsoft.AspNetCore.WebUtilities;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static string? AddQueryString(string uri, string? name, string? value) =>
   (IsNotEmptyString(name) && IsNotEmptyString(value))? QueryHelpers.AddQueryString(uri, name!, value!): default;

  public static string AddQueryString(string uri, IEnumerable<KeyValuePair<string, string?>> queryString) => QueryHelpers.AddQueryString(uri, queryString);
}