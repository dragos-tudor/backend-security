
# OpenIdConnect wip

## ASPNET Security libraries
- simplified ASPNET Security-like libraries.
- rewritten from scratch ASPNET Security libraries.
- "light" functional-style libraries [90% OOP-free].

### Security libraries
- [Authentication.Cookies](/Security.Authentication.Cookies/).
- [Authentication.Facebook](/Security.Authentication.Facebook/).
- [Authentication.Google](/Security.Authentication.Google/).
- [Authentication.Twitter](/Security.Authentication.Twitter/).
- [Authentication.BearerToken](/Security.Authentication.BearerToken/).
- [Authentication.OAuth2](/Security.Authentication.OAuth/).
- [Authentication.OpenIdConnect](/Security.Authentication.OpenIdConnect/).
- [Authorization](/Security.Authorization/).
- [DataProtection](/Security.DataProtection/).

### Design
- security services [authentication and authorization services] represent the **mechanism backbone**.
- security services [*high-level* functions] act as **security behaviour controllers** and represent public API.
- security libraries were written following some of **FP principles** [pure functions, high-order functions, immutability, data/behaviour separation, static methods/functions as first-class citiziens, result pattern].
- DI is used as **thin layer** usually over functional security services [eg. *SignInCookie* have 2 implementations with/without DI services]. DI services implementations are registred as usual with specific method extensions [eg. *AddCookiesServices*, *AddFacebookServices*].
- **security mechanism** is based on security services [authentication scheme free-mechanism]:
  - *authentication middleware* receive authentication service as param [*UseAuthentication* extension].
  - *authorization middleware* receive challenge and forbid services as params [*UseAuthorization* extension].
  - *oauth callback endpoints* receive signin service as param [eg. *MapFacebook*].
- authentication libraries implement specific authentication services [eg. *AuthenticateCookie*, *SignInCookie*, *ChallengeGoogle*, *AuthenticateFacebook*].
- authorization library implement authorization services [eg. *Authorize*].
- *high-level* functions usually use declarative style [eg. *SignInCookie*].
  - usually impure functions [with side-effects].
  - built on top of *low-level* and *intermediate-level* functions.
- *intermediate-level* functions use imperative/declarative style [eg. *SetAuthorizationParams*].
- *low-level* functions usually use imperative style and are one-liners [eg. *IsSecuredCookie*].
  - usually pure [without side effects] or semi-pure functions [side effects on parameters].
- *high-intermediate-low-level* hierarchy design I named it **lego principle**. It could be seen also as a **functions pyramid** having at the base *low-level* functions.
- **no else** strategy [0 (zero) else branches].

### Processes
- there are 2 different security processes: *local authentication* and *remote authentication*.
- *local authentication process* [cookie]:
  - each request [when use authentication middlware] call *authentication func* [eg. *AuthenticateCookie*]. Based on authentication result the middleware set *HttpContext.User* prop.
  - then each request [when use authorization middlware] call *authorization func* [eg. *Authorize*]. Based on authorization policies result is decided if the request is allowed, unauthenticated/challenged or unauthorized/forbidden.
  - signin/signout funcs are used on specific endpoints/controller actions implememted by devs.
- *remote authentication process* [OAuth2 protocol]:
  - when called, the *challenge endpoint* [eg. registered with *MapFacebook*] build and send an authorization request to authorization server.
  - after processing the authorization request the authorization server redirect response to *callback endpoint* [eg. registered with *MapFacebook*]. That endpoint receive authorization server response and call *callback func* [eg. *CallbackFacebook*, *CallbackOAuth*]. The *callback func* has 2 steps:
    - authentication: *AuthenticateOAuth* oauth authentication func has 3 substeps:
      * *PostAuthorization* - validate the authorization code and the request from the authorization server [local].
      * *ExchangeCodeForTokens* - exchange with the authorization server the authorization code for the access [and refresh] tokens [remote].
      * *AccessUserInfo* - using access token gets from the authorization server the user informations [remote].
      * The authentication step transform user informations received from authorization server into security claims, add them to the claims identity, create an authentication ticket and return the *AuthenticationResult*.
    - signin: after oauth authentication step when authentication succedded then the signin func is called [eg. *SiginInCookie^, *SignInBearerToken*]. Signin func is set on oauth endpoints registration.
  - after callback redirection next requests will use *local authentication process*.

### Remarks
- *completely* rewritten authentication mechanism.
- *partially* rewritten authorization mechamism [keeping compatibility with ASPNET authorization policies mechanism].
- cookie authentication services *surgically* implement session-based cookies feature [using *IsSessionBasedCookie* func]. Authenticating, signingin and signingout services are completely independent each other [no dependencies on HttpContext features]. *AuthenticationSessionCookie*, *SignInSessionCookie* and *SignOutSessionCookie* session-based cookies services are completely isolated from non-session based versions.
- authentication options implementation contains only data [eg. *CookieAuthenticationOptions*]. Cookie authentication services [non DI-based ones] receive all dependencies as parameters.
- Microsoft ASPNET authentication options implementation contains data and behaviour/services [eg. *SessionStore*, *TicketDataFormat*, *SystemClock* for *CookieAuthenticationOptions*]. This design have some advantages comparing with my implementation allowing options:
  - to have different services from those registered on DI.
  - to encapsulate and carry on those services through the authentication process [reducing the number of parameters so].
- *AuthenticateOAuth* oauth authentication func use template method design pattern allowing oauth libraries to override/decorate when neccessary *postAuthenticate*, *exchangeCodeForTokens* or *accessUserInfo* authentication substeps [eg. *AuthenticateTwitter*, *AuthenticateFacebook*].
- redirecting remarks:
  - *ChallengeOAuth* and *ChallengeOidc* funcs redirect to authorization server [*ChallengeOidc* could use form instead of redirection].
  - *CallbackOAuth* and *CallbackOidc* funcs redirect to original url or when callback authentication error to *AccessDeniedPath* or *ErrorPath* authentication options props depending of error type.
  - *SigninCookie*, *SignOutCookie*, *Challenge**, *Forbid** etc. no redirections [webapi oriented functionality]. when redirections are neccessary those funcs could be decorated and redirected to *AuthenticationProperties.RedirectUri* or to *AuthenticationOptions.ReturnUrlParameter* query parameter.
- cookies remarks:
  - *AuthenticationCookieOptions.ExpiresAfter* single place to control *AuthenticationTicket* [cookies] persistency.
  - *AuthenticationCookieOptions.CookieName* single place to control cookies names.
- oidc remarks:
  - *pkce* is the recommended solution regarding security for *authorization code* flow.
  - *implicit* and *hybrid* flows not supported based on oidc best practices [even supported by [oidc rfc](https://openid.net/specs/openid-connect-core-1_0.html#AuthRequest)].
  - *nonce* is not neccessary because *implicit* and *hybrid* are only flows with required *nonce* parameter.

### Project goals
- to untangle/demystify the ASPNET authentication/authorization mechanisms and local/remote processes.
- to simplify authentication/authorization mechanisms [ASPNET schema-based free mechanism].
- to demonstrate a functional programming implementation.
- to demonstrate a practical alternative to OOP.

### Benchmark
| Method    | InvocationCount | Mean      | Error     | StdDev    | Median    | Ratio | RatioSD | Gen0    | Gen1    | Gen2    | Allocated | Alloc Ratio |
|---------- |---------------- |----------:|----------:|----------:|----------:|------:|--------:|--------:|--------:|--------:|----------:|------------:|
| FPSignin  | 128             |  64.34 μs |  1.196 μs |  1.119 μs |  64.69 μs |  1.00 |    0.00 |       - |       - |       - |   7.96 KB |        1.00 |
| OOPSignin | 128             |  79.98 μs |  3.247 μs |  9.212 μs |  79.56 μs |  1.13 |    0.14 |  7.8125 |  7.8125 |  7.8125 | 116.21 KB |       14.59 |
|           |                 |           |           |           |           |       |         |         |         |         |           |             |
| FPSignin  | 512             |  45.75 μs |  5.065 μs | 14.934 μs |  39.12 μs |  1.00 |    0.00 |  1.9531 |       - |       - |   7.96 KB |        1.00 |
| OOPSignin | 512             |  97.08 μs |  7.432 μs | 21.679 μs |  95.52 μs |  2.41 |    1.12 |  9.7656 |  9.7656 |  9.7656 |  445.7 KB |       56.01 |
|           |                 |           |           |           |           |       |         |         |         |         |           |             |
| FPSignin  | 1024            |  28.83 μs |  2.009 μs |  5.533 μs |  26.26 μs |  1.00 |    0.00 |  1.9531 |       - |       - |   7.95 KB |        1.00 |
| OOPSignin | 1024            | 186.15 μs | 26.776 μs | 78.949 μs | 211.04 μs |  6.32 |    3.02 | 14.6484 | 13.6719 | 13.6719 | 915.64 KB |      115.12 |
- for InvocationCount > 2048 OOP benchmark start running extremely slow.
