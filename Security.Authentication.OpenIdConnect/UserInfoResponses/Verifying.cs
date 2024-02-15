
using System.Net.Http;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static bool IsJsonContentTypeUserInfoResponse(string? contentType) =>
    string.Equals(contentType, "application/json", StringComparison.OrdinalIgnoreCase);

  static bool IsJwtContentTypeUserInfoResponse(string? contentType) =>
    string.Equals(contentType, "application/jwt", StringComparison.OrdinalIgnoreCase);

  static bool IsValidContentTypeUserInfoResponse(string? contentType) =>
    IsJsonContentTypeUserInfoResponse(contentType) || IsJwtContentTypeUserInfoResponse(contentType);
}