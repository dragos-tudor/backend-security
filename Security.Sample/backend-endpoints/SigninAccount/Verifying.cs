
namespace Security.Sample.Endpoints;

partial class EndpointsFuncs
{
  static bool VerifyCredentials (CredentialsRequest credentials) =>
    (credentials.UserName == "user" && credentials.Password == "pass") ||
    (credentials.UserName == "username" && credentials.Password == "password");
}