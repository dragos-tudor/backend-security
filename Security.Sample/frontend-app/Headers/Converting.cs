
using System;
using System.Globalization;

namespace Security.Sample.App;

partial class AppFuncs
{
  static string ToSecondsString (TimeSpan interval) => interval.Seconds.ToString(CultureInfo.InvariantCulture);

  static string ToDayDateTimeString (DateTime date) => date.ToString("R", CultureInfo.InvariantCulture);
}