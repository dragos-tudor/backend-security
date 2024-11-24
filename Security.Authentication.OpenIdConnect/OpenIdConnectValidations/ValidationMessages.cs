// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Security.Authentication.OpenIdConnect;

static class ValidationMessages
{
  internal const string IDX21300 = "IDX21300: The hash claim: '{0}' in the id_token did not validate with against: '{1}', algorithm: '{2}'.";
  internal const string IDX21301 = "IDX21301: The algorithm: '{0}' specified in the jwt header could not be used to create a '{1}'. See inner exception for details.";
  internal const string IDX21302 = "IDX21302: The algorithm: '{0}' specified in the jwt header is not supported.";
  internal const string IDX21303 = "IDX21303: Validating hash of OIDC response. Expected: '{0}'.";
  internal const string IDX21304 = "IDX21304: Validating 'c_hash' using id_token and code.";
  internal const string IDX21305 = "IDX21305: Code received from Authorization Endpoint is null, there is no 'code' to validate.";
  internal const string IDX21306 = "IDX21306: The 'c_hash' claim was not a string in the 'id_token', but a 'code' was in the response, 'id_token': '{0}'.";
  internal const string IDX21307 = "IDX21307: The 'c_hash' claim was not found in the id_token, but a 'code' was in the response, id_token: '{0}'";
  internal const string IDX21308 = "IDX21308: 'azp' claim exists in the 'id_token' but 'client_id' is null. Cannot validate the 'azp' claim.";
  internal const string IDX21309 = "IDX21309: Validating 'at_hash' using id_token and access_token.";
  internal const string IDX21310 = "IDX21310: AccessToken received from Token Endpoint is null, there is no 'token' in the response to validate.";
  internal const string IDX21311 = "IDX21311: The 'at_hash' claim was not a string in the 'id_token', but an 'access_token' was in the response, 'id_token': '{0}'.";
  internal const string IDX21312 = "IDX21312: The 'at_hash' claim was not found in the 'id_token', but a 'access_token' was in the response, 'id_token': '{0}'.";
  internal const string IDX21313 = "IDX21313: The id_token: '{0}' is not valid. Delegate threw exception, see inner exception for more details.";
  internal const string IDX21314 = "IDX21314: OpenIdConnect protocol validator requires the jwt token to have an '{0}' claim. The jwt did not contain an '{0}' claim, jwt: '{1}'.";
  internal const string IDX21315 = "IDX21315: RequireAcr is 'true' (default is 'false') but jwt.PayLoad.Acr is 'null or whitespace', jwt: '{0}'.";
  internal const string IDX21316 = "IDX21316: RequireAmr is 'true' (default is 'false') but jwt.PayLoad.Amr is 'null or whitespace', jwt: '{0}'.";
  internal const string IDX21317 = "IDX21317: RequireAuthTime is 'true' (default is 'false') but jwt.PayLoad.AuthTime is 'null or whitespace', jwt: '{0}'.";
  internal const string IDX21318 = "IDX21318: RequireAzp is 'true' (default is 'false') but jwt.PayLoad.Azp is 'null or whitespace', jwt: '{0}'.";
  internal const string IDX21329 = "IDX21329: RequireState is '{0}' but the response state is null. State cannot be validated.";
  internal const string IDX21330 = "IDX21330: RequireState is '{0}', the request contained 'state', but the response does not contain 'state'.";
  internal const string IDX21331 = "IDX21331: The 'state' parameter in the message: '{0}', does not equal the 'state' in the context: '{1}'.";
  internal const string IDX21332 = "IDX21332: ValidatedIdToken is null. There is no 'id_token' to validate against.";
  internal const string IDX21336 = "IDX21336: Both 'id_token' and 'access_token' should be present in the response received from Token Endpoint";
  internal const string IDX21337 = "IDX21337: Response received from UserInfo Endpoint is null, there is no response to validate.";
  internal const string IDX21338 = "IDX21338: Subject claim present in 'id_token': '{0}' does not match the claim received from UserInfo Endpoint: '{1}'.";
  internal const string IDX21339 = "IDX21339: The 'id_token' contains multiple audiences but 'azp' claim is missing.";
  internal const string IDX21340 = "IDX21340: The 'id_token' contains 'azp' claim but its value is not equal to Client Id. 'azp': '{0}'. clientId: '{1}'.";
  internal const string IDX21343 = "IDX21343: Unable to parse response from UserInfo endpoint: '{0}'";
  internal const string IDX21345 = "IDX21345: UserInfo Endpoint response does not contain a 'sub' claim, cannot validate.";
  internal const string IDX21346 = "IDX21346: ValidatedIdToken does not contain a 'sub' claim, cannot validate.";
  internal const string IDX21347 = "IDX21347: Validating the 'c_hash' failed, see inner exception.";
  internal const string IDX21348 = "IDX21348: Validating the 'at_hash' failed, see inner exception.";
  internal const string IDX21350 = "IDX21350: The algorithm specified in the jwt header is null or empty.";
}

