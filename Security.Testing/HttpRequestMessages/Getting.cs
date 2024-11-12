
using System.Text.RegularExpressions;

namespace Security.Testing;

partial class Funcs
{
  [GeneratedRegex(@"\w+=(?<content>[\w_-]+)(?=;)", RegexOptions.None)]
  private static partial Regex CookieContentRegex();

  internal static string GetRequestPath(HttpRequestMessage request) => request.RequestUri!.PathAndQuery.Split("?")[0];

  public static string? GetRequestMessageContent(HttpRequestMessage request) => request.Content?.ReadAsStringAsync().Result;

  public static string? GetRequestMessageCookieContent(string? cookie) => cookie is not null? CookieContentRegex().Match(cookie).Groups["content"].Value: default;

  public static(string, string) GetRequestMessageCookieHeader(HttpResponseMessage response) => ("cookie", GetResponseMessageCookie(response)!);
}

