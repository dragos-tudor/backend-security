
namespace Security.Authentication.OAuth;

public abstract record ClaimMapper(string ClaimType, string ClaimValueType = "string");

