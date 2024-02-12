using System.Threading;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static Task<IFormCollection> ReadRequestForm(HttpRequest request, CancellationToken cancellationToken = default) =>
    request.ReadFormAsync(cancellationToken);
}