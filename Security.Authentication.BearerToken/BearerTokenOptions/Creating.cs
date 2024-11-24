
using Microsoft.AspNetCore.Authentication.BearerToken;

namespace Security.Authentication.BearerToken;

partial class BearerTokenFuncs
{
  static readonly TimeSpan DefaultBearerTokenExpiration = TimeSpan.FromHours(1);
  static readonly TimeSpan DefaultRefreshTokenExpiration = TimeSpan.FromDays(14);

  public static BearerTokenOptions CreateBearerTokenOptions(string schemeName = BearerTokenDefaults.AuthenticationScheme) =>
    new() {
      BearerTokenExpiration = DefaultBearerTokenExpiration,
      RefreshTokenExpiration = DefaultRefreshTokenExpiration,
      SchemeName = schemeName
    };

}
