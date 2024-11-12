
using System.Linq;
using System.Net.Http.Headers;

namespace Security.Testing;

partial class Funcs
{
  static HttpRequestHeaders SetRequestMessageHeader(HttpRequestHeaders headers,(string, string) header)
  {
    headers.Add(header.Item1, header.Item2);
    return headers;
  }

  static HttpRequestHeaders SetRequestMessageHeaders(HttpRequestMessage request, params(string, string)[] headers) => headers.Aggregate(request.Headers, SetRequestMessageHeader);
}

