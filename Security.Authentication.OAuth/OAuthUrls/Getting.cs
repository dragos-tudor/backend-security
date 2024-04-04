using System.Collections.Generic;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  static string GetAuthorizationUri (OAuthOptions options, IDictionary<string, string> authorizationParams) =>
    AddQueryString(options.AuthorizationEndpoint, authorizationParams!);
}