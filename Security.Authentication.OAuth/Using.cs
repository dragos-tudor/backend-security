global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Security.Claims;
global using System.Threading;
global using System.Threading.Tasks;
global using Microsoft.Extensions.Logging;
global using static Security.Authentication.AuthenticationFuncs;
global using static Security.Authentication.OAuth.OAuthBaseFuncs;
global using OAuthParamNames = Security.Authentication.OAuth.OAuthParameterNames;
global using static Security.Authentication.OAuth.OAuthFuncs;

namespace Security.Authentication.OAuth;

public static partial class OAuthFuncs { }

#if RELEASE
  public static class Program { public static void Main() {} }
#endif
