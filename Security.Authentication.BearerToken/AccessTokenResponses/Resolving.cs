using System.Text.Json.Serialization.Metadata;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.Options;

namespace Security.Authentication.BearerToken;

partial class BearerTokenFuncs
{
    static JsonTypeInfo<AccessTokenResponse> ResolveAccessTokenJsonTypeInfo(HttpContext context)
    {
        // Attempt to resolve options from DI then fall back to static options
        var typeInfo = ResolveService<IOptions<JsonOptions>>(context)?
          .Value?
          .SerializerOptions?
          .GetTypeInfo(typeof(AccessTokenResponse)) as JsonTypeInfo<AccessTokenResponse>;
        return typeInfo ?? AccessTokenResponseSerializerContext.Default.AccessTokenResponse;
    }
}