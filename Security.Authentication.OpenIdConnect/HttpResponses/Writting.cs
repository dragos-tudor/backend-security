using System.Text;
using System.Threading;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static ValueTask WriteResponseTextContent(
    HttpResponse response,
    string content,
    CancellationToken cancellationToken = default)
  {
    var buffer = Encoding.UTF8.GetBytes(content);
    SetResponseContentLength(response, buffer.Length);
    SetResponseContentType(response, "text/html;charset=UTF-8");
    return response.Body.WriteAsync(buffer, cancellationToken);
  }
}