
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OpenIdConnect;

public delegate Task<PostAuthorizeResult> PostAuthorizeFunc<TOptions>(
    HttpContext context,
    TOptions authOptions,
    OpenIdConnectValidationOptions validationOptions,
    PropertiesDataFormat authPropsProtector)
  where TOptions: OpenIdConnectOptions;