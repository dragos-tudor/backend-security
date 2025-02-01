
namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static string? GetCallbackAuthorizationState(HttpRequest request) => request.Query[OAuthParamNames.State];
}