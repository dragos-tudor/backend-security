using Microsoft.AspNetCore.Http.HttpResults;

namespace Security.Sample.Api;

partial class SampleFuncs
{
  static Ok<UserInfoDto> GetUserInfoEndpoint(HttpContext context) =>
    Ok(CreateUserInfo(context.User));
}