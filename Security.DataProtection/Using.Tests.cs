
global using Microsoft.VisualStudio.TestTools.UnitTesting;

[assembly: Parallelize(Scope = ExecutionScope.MethodLevel)]

namespace Security.DataProtection;

[TestClass]
public partial class DataProtectionTests { }