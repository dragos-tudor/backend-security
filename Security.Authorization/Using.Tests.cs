global using Microsoft.AspNetCore.TestHost;
global using static Security.Testing.Funcs;
global using Microsoft.VisualStudio.TestTools.UnitTesting;

[assembly: Parallelize(Scope = ExecutionScope.MethodLevel)]

namespace Security.Authorization;

[TestClass]
public partial class AuthorizationTests { }
[TestClass]
public partial class MicrosoftTests { }
