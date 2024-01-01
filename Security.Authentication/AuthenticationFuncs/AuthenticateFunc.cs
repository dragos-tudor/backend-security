
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication;

public delegate ValueTask<AuthenticateResult> AuthenticateFunc(HttpContext context);