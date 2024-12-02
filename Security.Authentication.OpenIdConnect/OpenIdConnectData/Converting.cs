
namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static OidcData ToOpenIdConnectData(IEnumerable<KeyValuePair<string, string[]>> pairs)
  {
    var result = CreateOidcData();

    foreach (var pair in pairs.Where(ExistKeyAndValue))
      if (FindFirstExistingValue(pair.Value) is string value)
        SetOidcData(result, pair.Key, value);

    return result;


    static bool ExistKeyAndValue(KeyValuePair<string, string[]> pair) => IsNotEmptyString(pair.Key) && pair.Value is not null;
    static string? FindFirstExistingValue(string[] values) => values.FirstOrDefault(IsNotEmptyString);
  }

  public static OidcData ToOpenIdConnectData(string jsonData) => (OidcData)new OpenIdConnectMessage(jsonData).Parameters;
}