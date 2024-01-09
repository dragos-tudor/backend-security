using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.BearerToken;

public interface IRefreshTokenProtector: ISecureDataFormat<AuthenticationTicket>;