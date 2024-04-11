
using System.Collections.Generic;

namespace Security.Authentication.Remote;

partial class RemoteFuncs
{
  public static readonly string CodeVerifier = "code_verifier";
  const string CodeChallenge = "code_challenge";
  const string CodeChallengeMethod = "code_challenge_method";
  const string CodeChallengeMethodS256 = "S256";

  public static string SetRemoteParam (IDictionary<string, string> remoteParams, string key, string value)
    { remoteParams.Add(key, value); return value; }

  static string SetRemoteParamCodeChallenge (IDictionary<string, string> remoteParams, string codeChallenge) =>
    SetRemoteParam(remoteParams, CodeChallenge, codeChallenge);

  static string SetRemoteParamCodeChallengeMethod (IDictionary<string, string> remoteParams) =>
    SetRemoteParam(remoteParams, CodeChallengeMethod, CodeChallengeMethodS256);

  public static string SetRemoteParamCodeVerifier (IDictionary<string, string> remoteParams, string codeVerifier) =>
    SetRemoteParam(remoteParams, CodeVerifier, codeVerifier);
}