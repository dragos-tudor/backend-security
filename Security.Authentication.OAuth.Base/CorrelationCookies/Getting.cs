
namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  public static string GetCorrelationCookieName(string correlationId) => $"{CorrelationCookieName}{correlationId}";

}