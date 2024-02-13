
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  internal const string ErrorDescriptionToken = "error_description";
  internal const string ErrorUriToken = "error_uri";
  internal const string EndpointError = "oauth endpoint failure";

  static string BuildGenericError (HttpRequest request) =>
    new StringBuilder(EndpointError)
      .AddErrorDetail("Description", request.Query[ErrorDescriptionToken])
      .AddErrorDetail("Uri", request.Query[ErrorUriToken])
      .ToString();

}