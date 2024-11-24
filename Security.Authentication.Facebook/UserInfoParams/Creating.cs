
using Security.Authentication.OAuth;

namespace Security.Authentication.Facebook;

partial class FacebookFuncs
{
  const string AppSecretProof = "appsecret_proof";
  const string Fields = "fields";

  static KeyValuePair<string, string?> CreateAccessTokenParam(string accessToken) => new(OAuthParameterNames.AccessToken, accessToken);

  static KeyValuePair<string, string?> CreateAppSecretProofParam(string appSecretProof) => new(AppSecretProof, appSecretProof);

  static KeyValuePair<string, string?> CreateFieldsParam(string fields) => new(Fields, fields);
}