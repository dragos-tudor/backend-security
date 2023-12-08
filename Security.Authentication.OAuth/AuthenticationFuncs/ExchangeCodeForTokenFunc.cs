
using System.Threading;
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OAuth;

public delegate Task<TokenResult> ExchangeCodeForTokensFunc<Options> (
  Options authOptions,
  AuthenticationProperties authProperties,
  string authCode,
  CancellationToken cancellationToken
) where Options: OAuthOptions;