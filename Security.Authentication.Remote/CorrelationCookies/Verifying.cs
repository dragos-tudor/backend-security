
namespace Security.Authentication.Remote;

partial class Funcs {

  static bool IsCorrelationContentMatch (string correlationContent) =>
    correlationContent == CorrelationCookieMarker;

  static bool IsEmptyCorrelationContent (string? correlationContent) =>
    correlationContent is null;

}