
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Cryptography.X509Certificates;
#nullable disable

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectTests {

  [TestMethod]
  public async Task Id_token_with_json_web_key__validate_token__validation_result() {

    // https://www.googleapis.com/oauth2/v3/certs
    var jsonStringKey = "{\"e\": \"AQAB\", \"kty\": \"RSA\", \"n\": \"tLZpmdBD-qb8fwqg-DKX8ljpCAAv5n9s5N-JBzOIu3Ry1au3diX_AXKcnpqWJt3Mh3lT4x-zKl4SLpcjpSHYdim4tmqKucUupLTXS-yIqGBw2xDaI0GpYd8QFiFAxTAcwrEoCdl3BGGojo4zmARcHBe_IfeQls097Um3Xu2uiD0RehagoXnDhzk54WAvN05GXJ1xzzx6B7H_fclXcUYb5p5n7SgPDUchTDsDFGCI60Sqqz10d_GNcceThotlXRXcGVlTQ9AGJ_ejzkLWE7NiJc7ZWkrufsNKvVsWT12y66u0VWeopuQZxqSoHIRvSZ71JsBT3dAN897ViZtyYdWoqQ\" }";
    var jsonKey = new JsonWebKey(jsonStringKey);

    var token = "eyJhbGciOiJSUzI1NiIsImtpZCI6IjhlMGFjZjg5MWUwOTAwOTFlZjFhNWU3ZTY0YmFiMjgwZmQxNDQ3ZmEiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL2FjY291bnRzLmdvb2dsZS5jb20iLCJhenAiOiI0MDc0MDg3MTgxOTIuYXBwcy5nb29nbGV1c2VyY29udGVudC5jb20iLCJhdWQiOiI0MDc0MDg3MTgxOTIuYXBwcy5nb29nbGV1c2VyY29udGVudC5jb20iLCJzdWIiOiIxMDg1NDQ2MjM5NTQ5NjMwODY5NDYiLCJlbWFpbCI6ImRyYWdvcy50dWRvci50ZXN0QGdtYWlsLmNvbSIsImVtYWlsX3ZlcmlmaWVkIjp0cnVlLCJhdF9oYXNoIjoibUp5VlRxMzlTejhPOXBGemVFbTRwUSIsImlhdCI6MTY3MjgzNjYzMCwiZXhwIjoxNjcyODQwMjMwfQ.a95jdXkoWpBpUNhhGVKGUDIiJdZyePnVoMvzCsWo8GhycTt2xKOsS8wfYGlhiDtRw6_5S0Qz6MnT68FJtAlYXAHZtRg9dzP5e7XhhtKC30yvKKaJhMzS8y_SHGhnT4gGZLtyMU8ITzjxP7ZPTpJg03Ikywh1nrDLUpaasNjBHyMqFbL-sajN_iVBbIZ2gaYe2484JEo4_zx8h5J7u4Ebc4_6FsVapn2QsKt-ETY3ZMjZvYoMfdAdVstcL51d-QKYX-NoIA1T5Wmv5phc12uK6CaA-Qp7uE6Q7UIuz2jj5osbgRlX1i4UCRFHKLMPsWvW9sjoMXCShYUiKBrHgeb_mw";
    var tokenHandler = new JsonWebTokenHandler();
    var tokenValidationParameters = new TokenValidationParameters {
      IssuerSigningKey = jsonKey,
      ValidateIssuerSigningKey = true,
      ClockSkew = TimeSpan.MaxValue,
      ValidateLifetime = true,
      ValidAudience = "407408718192.apps.googleusercontent.com",
      ValidateAudience = true,
      ValidIssuer = "https://accounts.google.com",
      ValidateIssuer = true
    };

    // verify signature jwt.io => RSASHA256(base64UrlEncode(header) + "." + base64UrlEncode(payload), <stringKey or string cert>).
    var tokenValidationResult = await tokenHandler.ValidateTokenAsync(token, tokenValidationParameters);
    Assert.IsTrue(tokenValidationResult.IsValid);

  }

  [TestMethod]
  public async Task Id_token_with_public_key__validate_token__validation_result() {

    // https://www.googleapis.com/oauth2/v1/certs
    var x509StringCert = "-----BEGIN CERTIFICATE-----\nMIIDJzCCAg+gAwIBAgIJALyHu59ha1ZiMA0GCSqGSIb3DQEBBQUAMDYxNDAyBgNV\nBAMMK2ZlZGVyYXRlZC1zaWdub24uc3lzdGVtLmdzZXJ2aWNlYWNjb3VudC5jb20w\nHhcNMjIxMjI1MTUyMjI3WhcNMjMwMTExMDMzNzI3WjA2MTQwMgYDVQQDDCtmZWRl\ncmF0ZWQtc2lnbm9uLnN5c3RlbS5nc2VydmljZWFjY291bnQuY29tMIIBIjANBgkq\nhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAtLZpmdBD+qb8fwqg+DKX8ljpCAAv5n9s\n5N+JBzOIu3Ry1au3diX/AXKcnpqWJt3Mh3lT4x+zKl4SLpcjpSHYdim4tmqKucUu\npLTXS+yIqGBw2xDaI0GpYd8QFiFAxTAcwrEoCdl3BGGojo4zmARcHBe/IfeQls09\n7Um3Xu2uiD0RehagoXnDhzk54WAvN05GXJ1xzzx6B7H/fclXcUYb5p5n7SgPDUch\nTDsDFGCI60Sqqz10d/GNcceThotlXRXcGVlTQ9AGJ/ejzkLWE7NiJc7ZWkrufsNK\nvVsWT12y66u0VWeopuQZxqSoHIRvSZ71JsBT3dAN897ViZtyYdWoqQIDAQABozgw\nNjAMBgNVHRMBAf8EAjAAMA4GA1UdDwEB/wQEAwIHgDAWBgNVHSUBAf8EDDAKBggr\nBgEFBQcDAjANBgkqhkiG9w0BAQUFAAOCAQEAJYBy50xOGWxPWo2SStoaBENyNDnK\nOnsic5hFA/ILHZc+/jJTzksXqlQodwly6L7gSCfnX/C4LF2LzxGbd3s+ntonUPnl\nixzpF9hfzuGnsugIQOHmDdnuZy1uwojJoKdg/LCAtiR6djHHtbkcJqgA4tOncAMf\n4XHn4oIV/N4K/PSek6WnlM2onRC0rc3yNbHyBBfeqP9TD6WGK0DFW/TleK7okQiB\nUT+G48rCl2V4e5RMY/E+R2xLop8s5G/x6CR8S5xiFTTqcapjlQ7g+my+hdLrj6BE\nk6sYcpYf0q/7gIcpdiTSdR2L2Hqp4pX4IBNCb6ZUsCA8R/bXwbGs7d2D8A==\n-----END CERTIFICATE-----\n";
    using var x509Cert = X509Certificate2.CreateFromPem(x509StringCert);
    var x509Key = new X509SecurityKey(x509Cert);

    var token = "eyJhbGciOiJSUzI1NiIsImtpZCI6IjhlMGFjZjg5MWUwOTAwOTFlZjFhNWU3ZTY0YmFiMjgwZmQxNDQ3ZmEiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL2FjY291bnRzLmdvb2dsZS5jb20iLCJhenAiOiI0MDc0MDg3MTgxOTIuYXBwcy5nb29nbGV1c2VyY29udGVudC5jb20iLCJhdWQiOiI0MDc0MDg3MTgxOTIuYXBwcy5nb29nbGV1c2VyY29udGVudC5jb20iLCJzdWIiOiIxMDg1NDQ2MjM5NTQ5NjMwODY5NDYiLCJlbWFpbCI6ImRyYWdvcy50dWRvci50ZXN0QGdtYWlsLmNvbSIsImVtYWlsX3ZlcmlmaWVkIjp0cnVlLCJhdF9oYXNoIjoibUp5VlRxMzlTejhPOXBGemVFbTRwUSIsImlhdCI6MTY3MjgzNjYzMCwiZXhwIjoxNjcyODQwMjMwfQ.a95jdXkoWpBpUNhhGVKGUDIiJdZyePnVoMvzCsWo8GhycTt2xKOsS8wfYGlhiDtRw6_5S0Qz6MnT68FJtAlYXAHZtRg9dzP5e7XhhtKC30yvKKaJhMzS8y_SHGhnT4gGZLtyMU8ITzjxP7ZPTpJg03Ikywh1nrDLUpaasNjBHyMqFbL-sajN_iVBbIZ2gaYe2484JEo4_zx8h5J7u4Ebc4_6FsVapn2QsKt-ETY3ZMjZvYoMfdAdVstcL51d-QKYX-NoIA1T5Wmv5phc12uK6CaA-Qp7uE6Q7UIuz2jj5osbgRlX1i4UCRFHKLMPsWvW9sjoMXCShYUiKBrHgeb_mw";
    var tokenHandler = new JsonWebTokenHandler();
    var tokenValidationParameters = new TokenValidationParameters {
      IssuerSigningKey = x509Key,
      ValidateIssuerSigningKey = true,
      ClockSkew = TimeSpan.MaxValue,
      ValidateLifetime = true,
      ValidAudience = "407408718192.apps.googleusercontent.com",
      ValidateAudience = true,
      ValidIssuer = "https://accounts.google.com",
      ValidateIssuer = true
    };

    var tokenValidationResult = await tokenHandler.ValidateTokenAsync(token, tokenValidationParameters);
    Assert.IsTrue(tokenValidationResult.IsValid);

  }

}

