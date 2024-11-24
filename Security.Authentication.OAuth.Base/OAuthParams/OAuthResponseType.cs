
namespace Security.Authentication.OAuth;

public static class OAuthResponseType
{
  public const string Code = "code";
  [Obsolete($"oauth2 best practices not recommended", true)]
  public const string Implicit = "token";
}