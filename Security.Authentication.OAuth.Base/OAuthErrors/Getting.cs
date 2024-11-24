
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  public const string ErrorTypeToken = "error";
  public const string ErrorDescriptionToken = "error_description";
  public const string ErrorUriToken = "error_uri";

  static string GetOAuthErrorQuery(string error) => $"{ErrorTypeToken}={error}";

  public static string? GetOAuthErrorDescription(HttpRequest request) => request.Query[ErrorDescriptionToken];

  public static string GetOAuthErrorType(HttpRequest request) => request.Query[ErrorTypeToken]!;

  public static string GetOAuthErrorType(JsonElement response) => response.GetString(ErrorTypeToken)!;

  public static string? GetOAuthErrorUri(HttpRequest request) => request.Query[ErrorUriToken];

  public static string GetOAuthRedirectUriWithError(AuthenticationProperties authProps, string error) => $"{GetOAuthRedirectUri(authProps)}?{GetOAuthErrorQuery(error)}";
}