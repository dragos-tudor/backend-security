using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static ValueTask WriteResponseTextContent(
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