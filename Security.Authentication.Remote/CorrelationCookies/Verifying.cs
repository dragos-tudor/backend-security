
namespace Security.Authentication.Remote;

partial class RemoteFuncs
{
  public static bool ExistsCorrelationCookieValidationError (string error) =>
    error is not null;

  static bool IsCorrelationContentMatch (string correlationContent) =>
    correlationContent == CorrelationCookieMarker;

  static bool IsEmptyCorrelationContent (string? correlationContent) =>
    correlationContent is null;

}