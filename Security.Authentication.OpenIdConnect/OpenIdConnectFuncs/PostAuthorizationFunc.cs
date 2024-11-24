
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OpenIdConnect;

public delegate Task<AuthorizationResult> PostAuthorizationFunc<TOptions>(HttpContext context, TOptions authOptions, PropertiesDataFormat propertiesDataFormat, StringDataFormat stringDataFormat) where TOptions: OpenIdConnectOptions;