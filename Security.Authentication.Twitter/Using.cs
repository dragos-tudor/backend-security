global using System;
global using System.Collections.Generic;
global using System.Security.Claims;
global using static Security.Authentication.AuthenticationFuncs;
global using static Security.Authentication.OAuth.OAuthFuncs;

namespace Security.Authentication.Twitter;

public static partial class TwitterFuncs { }

#if RELEASE
  public static class Program { public static void Main() {} }
#endif