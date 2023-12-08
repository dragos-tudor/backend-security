
namespace Security.Samples;

partial class Funcs {

  static string? GetRequestFormParam (HttpRequest request, string key, string? defaultValue = default) =>
    request.Form.ContainsKey(key)? request.Form[key]: defaultValue;

  static string? GetUserName (HttpRequest request) =>
    GetRequestFormParam(request, "user");

  static string? GetUserPassword (HttpRequest request) =>
    GetRequestFormParam(request, "password");

}