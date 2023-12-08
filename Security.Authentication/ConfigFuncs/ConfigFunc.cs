
using Microsoft.AspNetCore.Identity;
using Security.Authentication;

namespace Security.Authentication;

public delegate T ConfigFunc<T>(T options);