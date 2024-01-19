using System.Text;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static async ValueTask WriteResponseHtmlContent(HttpResponse response, string content)
  {
    var buffer = Encoding.UTF8.GetBytes(content);
    SetResponseContentLength(response, buffer.Length);
    SetResponseContentType(response, "text/html;charset=UTF-8");
    await response.Body.WriteAsync(buffer);
  }
}