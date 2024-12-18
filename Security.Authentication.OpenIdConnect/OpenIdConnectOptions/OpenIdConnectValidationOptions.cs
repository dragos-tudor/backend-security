using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace Security.Authentication.OpenIdConnect;

public delegate string? IdTokenValidator(OpenIdConnectValidationOptions validationOptions, JwtSecurityToken idToken);

public class OpenIdConnectValidationOptions
{
  public const bool RequireSubByDefault = true;

  public Dictionary<string, string> HashAlgorithmMap { get; init; } =
    new()
    {
      { SecurityAlgorithms.EcdsaSha256, "SHA256" },
      { SecurityAlgorithms.EcdsaSha256Signature, "SHA256" },
      { SecurityAlgorithms.HmacSha256, "SHA256" },
      { SecurityAlgorithms.RsaSha256, "SHA256" },
      { SecurityAlgorithms.RsaSha256Signature, "SHA256" },
      { SecurityAlgorithms.RsaSsaPssSha256, "SHA256" },
      { SecurityAlgorithms.EcdsaSha384, "SHA384" },
      { SecurityAlgorithms.EcdsaSha384Signature, "SHA384" },
      { SecurityAlgorithms.HmacSha384, "SHA384" },
      { SecurityAlgorithms.RsaSha384, "SHA384" },
      { SecurityAlgorithms.RsaSha384Signature, "SHA384" },
      { SecurityAlgorithms.RsaSsaPssSha384, "SHA384" },
      { SecurityAlgorithms.EcdsaSha512, "SHA512" },
      { SecurityAlgorithms.EcdsaSha512Signature, "SHA512" },
      { SecurityAlgorithms.HmacSha512, "SHA512" },
      { SecurityAlgorithms.RsaSha512, "SHA512" },
      { SecurityAlgorithms.RsaSha512Signature, "SHA512" },
      { SecurityAlgorithms.RsaSsaPssSha512, "SHA512" }
  };

  [DefaultValue(false)]
  public bool RequireAcr { get; init; }

  [DefaultValue(false)]
  public bool RequireAmr { get; init; }

  [DefaultValue(false)]
  public bool RequireAuthTime { get; init; }

  [DefaultValue(false)]
  public bool RequireAzp { get; init; }

  [DefaultValue(true)]
  public bool RequireState { get; init; }

  [DefaultValue(false)]
  public bool RequireStateValidation { get; init; }

  [DefaultValue(true)]
  public bool RequireSub { get; init; } = RequireSubByDefault;

  public IdTokenValidator? IdTokenValidator { get; init; }

  public CryptoProviderFactory CryptoProviderFactory  { get; init; } = CryptoProviderFactory.Default;
}