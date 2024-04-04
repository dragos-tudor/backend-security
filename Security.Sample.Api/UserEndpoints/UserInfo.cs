namespace Security.Sample.Api;

public record class UserInfoDto(string UserName, string SchemeName, string[] UserClaims);