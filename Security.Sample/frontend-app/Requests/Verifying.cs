using System;
using Microsoft.AspNetCore.Http;

namespace Security.Sample.App;

partial class AppFuncs
{
  static bool IsRouteRequest (HttpRequest request) =>
    !((string)request.Path).Contains('.', StringComparison.Ordinal);
}