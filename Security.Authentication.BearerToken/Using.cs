global using System;
global using System.Security.Claims;
global using System.Threading.Tasks;
global using Microsoft.Extensions.Logging;
global using static Security.Authentication.AuthenticationFuncs;
global using static Security.Authentication.BearerToken.BearerTokenFuncs;

namespace Security.Authentication.BearerToken;

public static partial class BearerTokenFuncs { }

#if RELEASE
  public static class Program { public static void Main() {} }
#endif

