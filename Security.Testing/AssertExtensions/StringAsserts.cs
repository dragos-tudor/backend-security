
namespace Microsoft.VisualStudio.TestTools.UnitTesting;

public static class StringAsserts
{
  const string AssertionFailed = "{0} failed. {1}";
  const string NotContainsFail = "String '{0}' contain string '{1}'. {2}";

  public static void NotContains(string? value, string? substring)
  {
    NotContains(value, substring, string.Empty, StringComparison.Ordinal);
  }

  public static void NotContains(string? value, string? substring, StringComparison comparisonType)
  {
    NotContains(value, substring, string.Empty, comparisonType);
  }

  public static void NotContains(string? value, string? substring, string? message)
  {
    NotContains(value, substring, message, StringComparison.Ordinal);
  }

  public static void NotContains(string? value, string? substring, string? message, StringComparison comparisonType)
  {
    NotContains(value, substring, message, comparisonType, string.Empty);
  }

  public static void NotContains(string? value, string? substring, string? message, params object?[]? parameters)
  {
    NotContains(value, substring, message, StringComparison.Ordinal, parameters);
  }

  public static void NotContains(string? value, string? substring, string? message, StringComparison comparisonType, params object?[]? parameters)
  {
    ArgumentException.ThrowIfNullOrEmpty(value, nameof(value));
    ArgumentException.ThrowIfNullOrEmpty(substring, nameof(substring));
    if (value.IndexOf(substring, comparisonType) >= 0)
    {
      // TODO: handle null parameters
      string arg = string.Format(message ?? string.Empty, parameters ?? Array.Empty<object>());
      string message2 = string.Format(NotContainsFail, value, substring, arg);
      ThrowAssertFailed("StringAssert.NotContains", message2);
    }
  }

  internal static void ThrowAssertFailed(string assertionName, string? message)
  {
    throw new AssertFailedException(string.Format(AssertionFailed, assertionName, message));
  }
}