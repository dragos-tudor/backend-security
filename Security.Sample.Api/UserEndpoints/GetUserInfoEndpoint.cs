namespace Security.Sample.Api;

partial class SampleFuncs
{
  static UserInfoDto GetUserInfoEndpoint(HttpContext context) =>
    CreateUserInfo(context.User);
}