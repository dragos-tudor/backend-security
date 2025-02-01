
namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static string? GetAuthorizationState(HttpRequest request) => request.Query[OAuthParamNames.State];
}