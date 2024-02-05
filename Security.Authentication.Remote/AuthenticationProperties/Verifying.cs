
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.Remote;

partial class RemoteFuncs {

  static bool ExistsAuthenticationPropertyItem (AuthenticationProperties authProperties, string key) =>
    authProperties.Items.ContainsKey(key);

}