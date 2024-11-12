
using Microsoft.AspNetCore.Authentication.BearerToken;

namespace Security.Authentication.BearerToken;

partial class BearerTokenFuncs {

  public static BearerTokenOptions CreateBearerTokenOptions(string schemeName = BearerTokenDefaults.AuthenticationScheme) =>
    new() {
      BearerTokenExpiration = TimeSpan.FromHours(1),
      RefreshTokenExpiration = TimeSpan.FromDays(14),
      SchemeName = schemeName
    };

}
