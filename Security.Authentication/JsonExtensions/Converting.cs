
using System.Linq;
using System.Security.Claims;
using System.Text.Json;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static IDictionary<string, string> ToJsonDictionary(JsonElement elem)
  {
    var dictionary = new Dictionary<string, string>();
    if (!IsJsonObjectType(elem)) return dictionary;

    foreach (var prop in GetValidJsonProperties(elem))
    {
      if (IsJsonObjectType(prop))
        foreach (var subProp in GetValidJsonProperties(prop.Value))
          AddJsonProperty(dictionary, subProp, prop.Name);

      if (!IsJsonObjectType(prop))
        AddJsonProperty(dictionary, prop);
    }

    return dictionary;
  }
}