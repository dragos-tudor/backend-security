
namespace Security.Authentication.OAuth;

public abstract record ClaimAction(string ClaimType, string ClaimValueType = "string");

