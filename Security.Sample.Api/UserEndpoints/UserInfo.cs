using System.Collections.Generic;

namespace Security.Sample.Api;

public record class UserInfoDto(string UserName, string SchemeName, IEnumerable<string> UserClaims);