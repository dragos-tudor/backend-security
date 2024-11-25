
using System.Globalization;
using Security.Testing;

namespace Microsoft.VisualStudio.TestTools.UnitTesting;

public static class StringAsserts
{
  public static void NotContains(string? value, string? substring) => NotContains(value, substring, string.Empty, StringComparison.Ordinal);

  public static void NotContains(string? value, string? substring, StringComparison comparisonType) => NotContains(value, substring, string.Empty, comparisonType);

  public static void NotContains(string? value, string? substring, string? message) => NotContains(value, substring, message, StringComparison.Ordinal);

  public static void NotContains(string? value, string? substring, string? message, StringComparison comparisonType) => NotContains(value, substring, message, comparisonType, string.Empty);

  public static void NotContains(string? value, string? substring, string? message, params object?[]? parameters) => NotContains(value, substring, message, StringComparison.Ordinal, parameters);

  public static void NotContains(string? value, string? substring, string? message, StringComparison comparisonType, params object?[]? parameters)
  {
    ArgumentException.ThrowIfNullOrEmpty(value, nameof(value));
    ArgumentException.ThrowIfNullOrEmpty(substring, nameof(substring));
    if (value.Contains(substring, comparisonType))
    {
      // TODO: handle null parameters
      var formatProvider = CultureInfo.InvariantCulture;
      string arg = string.Format(formatProvider, message ?? string.Empty, parameters ?? Array.Empty<object>());
      string message2 = $"String '{value}' contain string '{substring}'. {arg}";
      ThrowAssertFailed("StringAssert.NotContains", message2);
    }
  }

  internal static void ThrowAssertFailed(string assertionName, string? message) { throw new AssertFailedException($"{assertionName} failed. {message}"); }
}