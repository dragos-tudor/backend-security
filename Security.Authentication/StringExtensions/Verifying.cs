
namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static bool IsEmptyString(string? value) => string.IsNullOrEmpty(value);

  public static bool IsNotEmptyString(string? value) => !string.IsNullOrEmpty(value);
}