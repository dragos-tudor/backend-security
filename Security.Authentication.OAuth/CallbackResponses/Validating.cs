
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  internal const string MissingAuthorizationCode = "missing authorization code from Authoriation endpoint";
  internal const string MissingState = "missing state from Authorization endpoint";
  internal const string UnprotectStateFailed = "unprotect oauth state failed";

  internal static string? ValidateCallbackResponse(HttpRequest request)
  {
    if (!ExistsCallbackAuthorizationCode(request)) return MissingAuthorizationCode;
    if (!ExistsCallbackState(request)) return MissingState;
    return default;
  }
}