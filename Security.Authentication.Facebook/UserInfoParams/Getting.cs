
namespace Security.Authentication.Facebook;

partial class FacebookFuncs
{
  static KeyValuePair<string, string?> GetUserInfoAppSecretProofParam(FacebookOptions facebookOptions, string accessToken) =>
    ShouldSendAppSecretProof(facebookOptions) ?
      CreateAppSecretProofParam(GenerateAppSecretProof(facebookOptions.ClientSecret, accessToken)) :
      default;

  static KeyValuePair<string, string?> GetUserInfoExtraFieldsParam(FacebookOptions facebookOptions) =>
    HasExtraFields(facebookOptions) ?
      CreateFieldsParam(string.Join(",", facebookOptions.Fields)) :
      default;
 }