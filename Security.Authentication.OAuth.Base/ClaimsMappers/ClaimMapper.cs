
namespace Security.Authentication.OAuth;

public abstract record ClaimMapper(string ClaimType, string KeyName, string ClaimValueType = "string");

