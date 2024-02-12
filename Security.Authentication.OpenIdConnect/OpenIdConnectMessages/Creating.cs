using System.Collections.Generic;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static OpenIdConnectMessage CreateOpenIdConnectMessage() =>  new();

  static OpenIdConnectMessage CreateOpenIdConnectMessage(string json) => new(json);

  static OpenIdConnectMessage CreateOpenIdConnectMessage(IEnumerable<KeyValuePair<string, string[]>> authParams) =>
    new(authParams);
}