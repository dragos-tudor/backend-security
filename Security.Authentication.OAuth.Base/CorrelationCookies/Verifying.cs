
namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  static bool IsCorrelationContentMatch(string correlationContent) => correlationContent == CorrelationCookieMarker;

  static bool IsEmptyCorrelationContent(string? correlationContent) => correlationContent is null;
}