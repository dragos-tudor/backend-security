
namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static OidcData ToOpenIdConnectData(IEnumerable<KeyValuePair<string, string[]>> pairs) =>
    pairs
      .Where(pair => IsNotEmptyString(pair.Key) && pair.Value is not null)
      .Where(pair => pair.Value.Any(IsNotEmptyString))
      .Aggregate(CreateOidcData(), (acc, pair) => {
        SetOidcData(acc, pair.Key, pair.Value.First(IsNotEmptyString));
        return acc;
      });

  public static OidcData ToOpenIdConnectData(string jsonData) => (OidcData)new OpenIdConnectMessage(jsonData).Parameters;
}