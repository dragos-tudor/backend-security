
namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static bool IsJsonContentTypeTokenResponse(string contentType) =>
    string.Equals(contentType, "application/json", StringComparison.OrdinalIgnoreCase);
}