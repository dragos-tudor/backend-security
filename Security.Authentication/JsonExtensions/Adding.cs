
using System.Linq;
using System.Security.Claims;
using System.Text.Json;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  static IDictionary<string, string> AddJsonProperty(IDictionary<string, string> dictionary, JsonProperty prop, string? keyPrefix = default)
  {
    if (IsJsonArrayType(prop))
      foreach (var elemPropValue in prop.Value.EnumerateArray())
        dictionary.Add(GetJsonKey(prop, keyPrefix), elemPropValue.ToString());

    if (!IsJsonArrayType(prop) && !IsJsonObjectType(prop))
      dictionary.Add(GetJsonKey(prop, keyPrefix), prop.Value.ToString());

    return dictionary;
  }
}