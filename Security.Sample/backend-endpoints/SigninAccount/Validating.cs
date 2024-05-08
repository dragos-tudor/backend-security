namespace Security.Sample.Endpoints;

partial class EndpointsFuncs
{
  static readonly CredentialsValidator CredentialsValidator =  new ();

  static string? ValidateCredentials (CredentialsRequest credentials)
  {
    var validationResult = CredentialsValidator.Validate(credentials);
    if (!validationResult.IsValid) return validationResult.ToString("\n");
    return default;
  }
}