
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.Remote;

partial class RemoteFuncs{

  static bool ExistsAuthenticationPropertyItem (AuthenticationProperties properties, string key) =>
    properties.Items.ContainsKey(key);

}