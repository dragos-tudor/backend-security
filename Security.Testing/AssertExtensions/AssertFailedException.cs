namespace Microsoft.VisualStudio.TestTools.UnitTesting;

[Serializable]
public sealed class AssertFailedException(string message) : Exception(message);