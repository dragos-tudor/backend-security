
namespace Security.Authentication.Twitter;

partial class TwitterFuncs {

  static string FormatClientCredentials (string clientId, string clientSecret) =>
    string.Concat(Uri.EscapeDataString(clientId), ":", Uri.EscapeDataString(clientSecret));

}