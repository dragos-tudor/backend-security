using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  static string GetAuthorizationUri (OAuthOptions options, IDictionary<string, string> authorizationParams) =>
    AddQueryString(options.AuthorizationEndpoint, authorizationParams!);
}