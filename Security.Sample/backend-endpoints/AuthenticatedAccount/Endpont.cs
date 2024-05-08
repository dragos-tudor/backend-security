using Microsoft.AspNetCore.Http.HttpResults;

namespace Security.Sample.Endpoints;

partial class EndpointsFuncs
{
  public static Ok<bool> AuthenticatedAccount(HttpContext context) =>
    Ok(IsAuthenticatedPrincipal(context.User));
}