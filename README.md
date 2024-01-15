## ASPNET Security libraries
- simplified ASPNET Security-like libraries.
- functional-style libraries [90% OOP-free].

### Security libraries
- [Authentication.Cookies](/Security.Authentication.Cookies/).
- [Authentication.Facebook](/Security.Authentication.Facebook/).
- [Authentication.Google](/Security.Authentication.Google/).
- [Authentication.Twitter](/Security.Authentication.Twitter/).
- [Authentication.BearerToken](/Security.Authentication.BearerToken/).
- [Authorization](/Security.Authorization/).
- [DataProtection](/Security.DataProtection/).

### Design
- security services [authentication and authorization services] represent the **mechanism backbone**.
- security services [*high-level* functions] act as **security behaviour controllers** and use *low-level* functions.
- security libraries were written following **FP principles** [pure functions, high-order functions, immutability, data/behaviour separation, static methods/functions as first-class citiziens].
- DI is used as **thin layer** usually over functional security services [eg. *SignInCookie* have 2 implementations with/without DI services]. DI services implementations are registred as usual with specific method extensions [eg. *AddCookies*, *AddFacebook*].
- **security mechanism** is based on security services [authentication scheme free-mechanism]:
  - *authentication middleware* receive authentication service [*UseAuthentication* extension].
  - *authorization middleware* receive challenge and forbid services [*UseAuthorization* extension].
  - *oauth callback endpoints* receive signin service [eg. *MapFacebook*].
- *high-level* functions usually use declarative style and TDA principle [eg. *SignInCookie*]. *High-level* functions, usually impure functions [with side-effects], are built on top of *low-level* functions, usually pure or semi-pure functions [some parameters are changed]. I named this kind of design *Lego principle*.
- *low-level* functions usually use imperative style and are one-liners [eg. *IsSecuredCookie*].
- authentication libraries implement specific authentication services [eg. *AuthenticateCookie*, *SignInCookie*, *ChallengeGoogle*, *AuthenticateFacebook*].
- authorization library implement authorization services [eg. *Authorize*].

### Processes
- there are 2 different authentication processes: local and remote.
- *local authenticaiton process* (cookie/bearer token):
  - each request [when use authentication middlware] goes through *authentication func* [eg. *AuthenticateCookie*]. Based on authorization result the middleware set *HttpContext.User* prop.
  - then each request [when use authentication middlware] goes through *authorization func* [eg. *Authorize*]. Based on authorization policies result is decided if the request is allowed, unauthenticated/challenged or unauthorized/forbidden.
  - signin/signout funcs are used on specific endpoints/controller actions implememted by programmer.
- *remote authentication process* (oauth):
  - when called the *challenge endpoint* [registered with eg. MapFacebook] build and send authoriztion request to authorization server.
  - then the authorization server redirect response to *callback endpoint* [registered with eg. MapFacebook]. That endpoint receive authorization server response and call *callback func* [eg. *ChallengeFacebook*]. The *callback func* has 2 steps:
    - authentication: oauth authentication func [*AuthenticateOAuth*] has 3 substeps:
      * *PostAuthorize* - validate the authentication code and the request from the authorization server [local].
      * *ExchangeCodeForTokens* - exchange with the authorization server the authentication code for the access [and refresh] tokens [remote].
      * *AccessUserInfo* - using access token gets from the authorization server the user informations [remote].
    - signin: after oauth authentication step for succedded *AuthenticationResult* the signin func is called [eg. *SiginInCookie^, *SignInBearerToken*]. Signin func is set on oauth endpoints registration.
  - after callback redirection next requests will goes through the *local authentication process*.

### Remarks
- *completely* rewritten authentication mechanism.
- *partially* rewritten authorization mechamism [keeping compatibility with ASPNET authorization policies mechanism].
- cookie authentication services *surgically* implement session-based cookies feature [using *IsSessionBasedCookie* func]. Authenticating, signingin and signingout services are completely independent each other [no dependencies on HttpContext features]. Authentication session-based cookies service is completely isolated.
- authentication options implementation contains only data [eg. *CookieAuthenticationOptions*]. Cookie authentication services [non DI-based ones] receive all dependencies as parameters.
- Microsoft ASPNET authentication options implementation contains data and behaviour/services [eg. *SessionStore*, *TicketDataFormat*, *SystemClock* for *CookieAuthenticationOptions*]. This design have some advantages comparing with my implementation allowing options:
  - to have different services from those registered on DI.
  - to encapsulate and carry on those services through the authentication flow [reducing the number of parameters so].
- *AuthenticateOAuth* oauth authentication func use template method design pattern allowing oauth libraries to override/decorate when neccessary *postAuthenticate*, *exchangeCodeForTokens* or *accessUserInfo* authentication substeps [eg. *AuthenticateTwitter*, *AuthenticateFacebook*].

### Project goals
- to untangle/demystify the ASPNET authentication/authorization mechanisms and local/remote authenticaiton processes.
- to simplify authentication/authorization mechanisms [ASPNET schema-based free mechanism].
- to demonstrate the superiority of functional programming paradigm over OOP paradigm.
- to demonstrate a practical alternative to OOP.

[wip OpenIdConnect, Sample.Api, Sample.www]
