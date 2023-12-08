
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authorization;

public delegate AuthenticateResult AuthenticateSchemeFunc(HttpContext context, string schemeName);