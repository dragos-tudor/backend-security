using System.Collections.Generic;

namespace Security.Sample.Endpoints;

public record class UserInfoResponse(string UserName, string SchemeName, IEnumerable<string> UserClaims);