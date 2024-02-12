using System.Globalization;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static double FloorMaxAgeSeconds (TimeSpan maxAge) =>
    Math.Floor(maxAge.TotalSeconds);

  static string ToOpenIdConnectMessageMaxAgeString (this TimeSpan maxAge) =>
    Convert.ToInt64(FloorMaxAgeSeconds(maxAge)).ToString(CultureInfo.InvariantCulture);
}