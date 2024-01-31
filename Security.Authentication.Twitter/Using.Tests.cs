
global using Microsoft.AspNetCore.TestHost;
global using Microsoft.VisualStudio.TestTools.UnitTesting;

[assembly: Parallelize(Scope = ExecutionScope.MethodLevel)]

namespace Security.Authentication.Twitter;

[TestClass]
public partial class TwitterTests { }