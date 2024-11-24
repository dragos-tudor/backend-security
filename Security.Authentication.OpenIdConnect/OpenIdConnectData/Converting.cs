
using System.Linq;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static OidcData ToOpenIdConnectData(IEnumerable<KeyValuePair<string, string[]>> entries)
  {
    var result = CreateOidcData();
    foreach (var entry in entries)
    {
      if (!ExistValueAndKey(entry)) continue;
      if (GetFirstValidValue(entry.Value) is string value) result[entry.Key] = value;
    }
    return result;

    static string? GetFirstValidValue(string[] values) => values.FirstOrDefault(value => value is not null);
    static bool ExistValueAndKey(KeyValuePair<string, string[]> entry) => entry.Value is not null && IsNotEmptyString(entry.Key);
  }
}