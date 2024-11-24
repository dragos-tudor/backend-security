global using System;
global using System.Collections.Generic;
global using System.Security.Claims;
global using System.Threading.Tasks;
global using System.Text;
global using Microsoft.AspNetCore.Authentication;
global using Microsoft.AspNetCore.Http;
global using Microsoft.Extensions.Logging;
global using Microsoft.IdentityModel.Protocols.OpenIdConnect;
global using Security.Authentication.OAuth;
global using static Security.Authentication.AuthenticationFuncs;
global using static Security.Authentication.OAuth.OAuthBaseFuncs;
global using static Security.Authentication.OpenIdConnect.OpenIdConnectFuncs;
global using OAuthParamNames = Security.Authentication.OAuth.OAuthParameterNames;
global using OidcParamNames = Microsoft.IdentityModel.Protocols.OpenIdConnect.OpenIdConnectParameterNames;
global using OidcDefaults = Security.Authentication.OpenIdConnect.OpenIdConnectDefaults;
global using OidcData = Security.Authentication.OpenIdConnect.OpenIdConnectData;

namespace Security.Authentication.OpenIdConnect;

public static partial class OpenIdConnectFuncs { }

#if RELEASE
  public static class Program { public static void Main() {} }
#endif
