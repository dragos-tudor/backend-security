using System.Globalization;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static double FloorMaxAgeSeconds(TimeSpan maxAge) =>
    Math.Floor(maxAge.TotalSeconds);

  static string ToMaxAgeString(TimeSpan? maxAge) =>
    Convert.ToInt64(FloorMaxAgeSeconds(maxAge!.Value)).ToString(CultureInfo.InvariantCulture);
}