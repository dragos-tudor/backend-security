global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Security.Claims;
global using static Security.Authentication.AuthenticationFuncs;
global using static Security.Authentication.OAuth.OAuthBaseFuncs;
global using OAuthParamNames = Security.Authentication.OAuth.OAuthParameterNames;

namespace Security.Authentication.OAuth;

public static partial class OAuthBaseFuncs { }

#if RELEASE
  public static class Program { public static void Main() {} }
#endif

