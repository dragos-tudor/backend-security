using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.BearerToken;

public interface IBearerTokenProtector: ISecureDataFormat<AuthenticationTicket>;