
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Remote;

partial class RemoteFuncs
{
  public const string ErrorDescriptionToken = "error_description";
  public const string ErrorUriToken = "error_uri";
  public const string EndpointError = "oauth endpoint failure";

  public static string BuildGenericError (HttpRequest request) =>
    new StringBuilder(EndpointError)
      .AddErrorDetail("Description", request.Query[ErrorDescriptionToken])
      .AddErrorDetail("Uri", request.Query[ErrorUriToken])
      .ToString();
}