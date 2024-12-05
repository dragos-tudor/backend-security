
using System.Text.Json;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  static string GetJsonKey(JsonProperty prop, string? keyPrefix = default) => keyPrefix is null? prop.Name: $"{keyPrefix}:{prop.Name}";

  static IEnumerable<JsonProperty> GetValidJsonProperties(JsonElement elem) => elem.EnumerateObject().Where(prop => !IsJsonUndefinedType(prop));
}