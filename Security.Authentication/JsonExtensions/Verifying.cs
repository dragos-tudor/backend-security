
using System.Text.Json;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  static bool IsJsonArrayType(JsonProperty prop) => prop.Value.ValueKind == JsonValueKind.Array;

  static bool IsJsonObjectType(JsonElement elem) => elem.ValueKind == JsonValueKind.Object;

  static bool IsJsonObjectType(JsonProperty prop) => prop.Value.ValueKind == JsonValueKind.Object;

  static bool IsJsonUndefinedType(JsonProperty prop) => prop.Value.ValueKind == JsonValueKind.Undefined;
}