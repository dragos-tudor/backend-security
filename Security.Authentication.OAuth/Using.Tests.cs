
global using Microsoft.VisualStudio.TestTools.UnitTesting;

[assembly: Parallelize(Scope = ExecutionScope.MethodLevel)]

namespace Security.Authentication.OAuth;

[TestClass]
public partial class OAuthTests { }