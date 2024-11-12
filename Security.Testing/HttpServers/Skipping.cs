
using Microsoft.AspNetCore.Builder;

namespace Security.Testing;

partial class Funcs
{
  const string AuthenticationMiddlewareSetKey = "__AuthenticationMiddlewareSet";
  const string AuthorizationMiddlewareSetKey = "__AuthorizationMiddlewareSet";

  static WebApplication SkipUsingAuthenticationMiddlewares(this WebApplication app)
  {
   ((IApplicationBuilder)app).Properties[AuthenticationMiddlewareSetKey] = true;
    return app;
  }

  static WebApplication SkipUsingAuthorizationMiddlewares(this WebApplication app)
  {
   ((IApplicationBuilder)app).Properties[AuthorizationMiddlewareSetKey] = true;
    return app;
  }
}

