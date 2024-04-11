
namespace Security.Authentication.Facebook;

partial class FacebookFuncs
{
  static KeyValuePair<string, string?>[] BuildFacebookUserInfoParams(
    FacebookOptions facebookOptions,
    string accessToken) =>
      [
        CreateAccessTokenParam(accessToken),
        GetUserInfoAppSecretProofParam(facebookOptions, accessToken),
        GetUserInfoExtraFieldsParam(facebookOptions)
      ];
 }