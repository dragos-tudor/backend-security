using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication;

public delegate ValueTask<string> SignOutFunc(
  HttpContext context,
  AuthenticationProperties? authProperties = default);
