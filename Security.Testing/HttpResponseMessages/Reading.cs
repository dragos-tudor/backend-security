
namespace Security.Testing;

partial class Funcs
{
  public static Task<string> ReadResponseMessageContent(HttpResponseMessage response) => response.Content.ReadAsStringAsync();
}

