
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  public static bool IsOAuthError(HttpRequest request) => IsNotEmptyString(GetOAuthErrorType(request));
}