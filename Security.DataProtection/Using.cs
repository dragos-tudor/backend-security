global using System;
global using System.IO;
global using static Security.DataProtection.DataProtectionFuncs;

namespace Security.DataProtection;

public static partial class DataProtectionFuncs { }

#if RELEASE
  public static class Program { public static void Main() {} }
#endif