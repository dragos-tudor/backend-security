global using static Security.Testing.Funcs;
global using Microsoft.VisualStudio.TestTools.UnitTesting;

[assembly: Parallelize(Scope = ExecutionScope.MethodLevel)]

namespace Security.Authentication.Remote;

[TestClass]
public partial class RemoteTests { }
