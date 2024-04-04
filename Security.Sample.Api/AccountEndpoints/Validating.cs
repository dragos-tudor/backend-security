namespace Security.Sample.Api;

partial class SampleFuncs
{
  static readonly CredentialsDtoValidator CredentialsValidator =  new ();
  const string InvalidUserNameOrPassword = "Invalid user name or password";

  static string? ValidateCredentials(CredentialsDto credentials)
  {
    var validationResult = CredentialsValidator.Validate(credentials);
    if (!validationResult.IsValid) return validationResult.ToString("\n");
    if (!CheckCredentials(credentials)) return InvalidUserNameOrPassword;
    return default;
  }
}