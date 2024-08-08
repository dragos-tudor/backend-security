namespace Security.Testing;

[Serializable]
public sealed class AssertFailedException: Exception
{
  public AssertFailedException() {}

  public AssertFailedException(string message): base(message) { }

  public AssertFailedException(string message, Exception innerException) : base(message, innerException){ }
}