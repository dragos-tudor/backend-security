using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication;

public delegate ValueTask<AuthenticationTicket> SignInFunc(HttpContext context, ClaimsPrincipal principal, AuthenticationProperties authProps);
