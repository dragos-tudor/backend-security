using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication;

public delegate ValueTask<bool> SignOutFunc(HttpContext context);
