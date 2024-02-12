
using System.Globalization;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  // https://www.w3.org/TR/xmlschema-2/#dateTime
  // https://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx
  static string ToAuthenticationTokenExpiresAtString (DateTimeOffset expiresAt) =>
    expiresAt.ToString("o", CultureInfo.InvariantCulture);

  static TimeSpan? ToAuthenticationTokenExpiresInTimeSpan (string expiresIn) =>
    int.TryParse(expiresIn, NumberStyles.Integer, CultureInfo.InvariantCulture, out int value)?
      TimeSpan.FromSeconds(value): default;
}