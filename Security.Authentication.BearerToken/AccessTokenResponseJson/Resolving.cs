using System.Text.Json.Serialization.Metadata;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.Options;

namespace Security.Authentication.BearerToken;

partial class BearerTokenFuncs
{
  static JsonTypeInfo<AccessTokenResponse> ResolveAccessTokenResponseJsonTypeInfo(HttpContext context) =>
    GetAccessTokenResponseJsonTypeInfo(ResolveService<IOptions<JsonOptions>>(context)) ??
    AccessTokenResponseJsonMetadata.Default.AccessTokenResponse;
}