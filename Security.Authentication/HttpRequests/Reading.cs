using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static Task<IFormCollection> ReadHttpRequestForm(HttpRequest request, CancellationToken cancellationToken = default) => request.ReadFormAsync(cancellationToken);
}