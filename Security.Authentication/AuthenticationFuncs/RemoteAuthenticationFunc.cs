
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication;

public delegate Task<bool> RemoteAuthenticateFunc(HttpContext context);