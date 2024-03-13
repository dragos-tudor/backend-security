namespace Security.Sample.Api;

partial class SampleFuncs
{
  const string InvalidUserNameOrPassword = "Invalid user name or password";

  static string? ValidateCredentials(CredentialsDto credentials) =>
    CheckCredentials(credentials)? default: InvalidUserNameOrPassword;
}