global using System;
global using System.Security.Claims;
global using System.Threading.Tasks;
global using Microsoft.Extensions.Logging;
global using static Security.Authentication.AuthenticationFuncs;
global using static Security.Authentication.Remote.RemoteFuncs;
global using static Security.Authentication.OpenIdConnect.OpenIdConnectFuncs;

namespace Security.Authentication.OpenIdConnect;

public static partial class OpenIdConnectFuncs { }

#if RELEASE
  public static class Program { public static void Main() {} }
#endif
