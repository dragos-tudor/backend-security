
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  internal const string AuthorizationErrorDescription = "error_description";
  internal const string AuthorizationErrorUri = "error_uri";
  internal const string AuthorizationEndpointError = "Authorization endpoint failure";

  static string BuildAuthorizationError (HttpRequest request) =>
    new StringBuilder(AuthorizationEndpointError)
      .AddAuthorizationErrorDetail("Description", request.Query[AuthorizationErrorDescription])
      .AddAuthorizationErrorDetail("Uri", request.Query[AuthorizationErrorUri])
      .ToString();

}