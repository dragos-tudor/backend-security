
namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static bool IsEmptyString(string? value) => string.IsNullOrEmpty(value);

  public static bool IsNotEmptyString(string? value) => !string.IsNullOrEmpty(value);

  public static bool EqualsStringOrdinal(string? value1, string? value2) => string.Equals(value1, value2, StringComparison.Ordinal);
}