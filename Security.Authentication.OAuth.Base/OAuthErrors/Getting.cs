
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

  static string? GetOAuthErrorDescription(HttpRequest request) => request.Query[ErrorDescriptionToken];

  static string GetOAuthErrorType(HttpRequest request) => request.Query[ErrorTypeToken]!;

  static string? GetOAuthErrorUri(HttpRequest request) => request.Query[ErrorUriToken];

  static string? GetOAuthErrorDescription(JsonElement data) => data.GetString(ErrorDescriptionToken)!;

  static string GetOAuthErrorType(JsonElement data) => data.GetString(ErrorTypeToken)!;

  static string? GetOAuthErrorUri(JsonElement data) => data.GetString(ErrorUriToken)!;

  public static OAuthError GetOAuthError(HttpRequest request) => CreateOAuthError(GetOAuthErrorType(request), GetOAuthErrorDescription(request), GetOAuthErrorUri(request));

  public static OAuthError GetOAuthError(JsonElement data) => CreateOAuthError(GetOAuthErrorType(data), GetOAuthErrorDescription(data), GetOAuthErrorUri(data));
}