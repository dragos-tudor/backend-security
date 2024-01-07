
using System.Linq;

namespace Security.Testing;

partial class Funcs {

  static IEnumerable<string> GetResponseMessageCookies (HttpResponseMessage response) =>
    GetResponseMessageHeader(response, "set-cookie");

  static IEnumerable<string> GetResponseMessageHeader (HttpResponseMessage response, string name) =>
    response.Headers.GetValues(name);

  public static string? GetResponseMessageCookie (HttpResponseMessage response) =>
    GetResponseMessageCookies(response).FirstOrDefault();

  public static string GetResponseMessageLocation (HttpResponseMessage response) =>
    WebUtility.UrlDecode(response.Headers.Location!.OriginalString);

}

