using System.Text.Json.Serialization.Metadata;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.Options;

namespace Security.Authentication.BearerToken;

partial class BearerTokenFuncs
{
  internal static JsonTypeInfo<AccessTokenResponse>? GetAccessTokenResponseJsonTypeInfo(IOptions<JsonOptions>? jsonOptions) =>
    jsonOptions?
      .Value
      .SerializerOptions?
      .GetTypeInfo(typeof(AccessTokenResponse)) as JsonTypeInfo<AccessTokenResponse>;
}