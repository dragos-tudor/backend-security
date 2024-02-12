
namespace Security.Authentication.OpenIdConnect;

public partial record class PostAuthorizeResult(PostAuthorizeInfo? PostAuthorizeInfo, string? Failure);