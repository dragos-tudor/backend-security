namespace Security.Sample.Api;

partial class SampleFuncs
{
  static bool CheckCredentials (CredentialsDto credentials) =>
    credentials.UserName == "username" && credentials.Password == "password";
}