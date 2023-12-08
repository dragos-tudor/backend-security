
namespace Security.Samples;

partial class Funcs {

  static bool ValidateUserCredentials (HttpRequest request) =>
    GetUserName(request) == "user" &&
    GetUserPassword(request) == "password";

}