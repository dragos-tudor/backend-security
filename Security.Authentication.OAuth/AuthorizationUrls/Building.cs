
using System.Collections.Generic;
using Microsoft.AspNetCore.WebUtilities;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  static string BuildAuthorizationUri (OAuthOptions options, IDictionary<string, string> authorizationParams) =>
    QueryHelpers.AddQueryString(options.AuthorizationEndpoint, authorizationParams!);

}