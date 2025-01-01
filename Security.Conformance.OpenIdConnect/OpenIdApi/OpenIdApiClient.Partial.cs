
using System.Text.Json;

namespace Security.Conformance.OpenIdConnect;

partial class OpenIdApiClient
{
  static partial void UpdateJsonSerializerSettings(JsonSerializerOptions settings) => settings.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

  internal static JsonSerializerOptions JsonSerializerOptions => _settings.Value;
}