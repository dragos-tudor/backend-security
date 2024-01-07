## ASPNET Security libraries
- simplified ASPNET Security-like libraries.
- functional-style libraries [90% OOP-free].

### Security libraries
- [Authentication.Cookies](/Security.Authentication.Cookies/).
- [Authentication.Facebook](/Security.Authentication.Facebook/).
- [Authentication.Google](/Security.Authentication.Google/).
- [Authentication.Twitter](/Security.Authentication.Twitter/).
- [Authorization](/Security.Authorization/).
- [DataProtection](/Security.DataProtection/).

### Design
- security services [authentication and authorization services] represent the **security backbone**.
- security services [*high-level* functions] act as **security behaviour controllers** and use *low-level* functions.
- security libraries were written following **FP principles** [pure functions, static methods as first-class citiziens, high-order functions, immutability, data/behaviour separation, *high-level* functions with side-effects].
- DI is used as **thin layer** usually over functional security services [eg. *SignInCookie* have 2 implementations with/without DI services]. DI services implementations are registred as usual with specific extensions [eg. *AddCookies*, *AddFacebook*].
- **security mechanism** is based on security services [authentication scheme free-mechanism]:
  - *authentication middleware* receive authentication service [*UseAuthentication* extension].
  - *authorization middleware* receive challenge and forbid services [*UseAuthorization* extension].
  - *oauth callback endpoints* receive signin service [eg. *MapFacebook*].
- *high-level* functions usually use declarative style and TDA principle [eg. *SignInCookie*].
- *low-level* functions usually use imperative style and are one-liners [eg. *IsSecuredCookie*].

### Services
- authentication libraries implement specific authentication services [eg. *AuthenticateCookie*, *SignInCookie*, *ChallengeGoogle*, *AuthenticateFacebook*].
- authorization library implement authorization services [eg. *Authorize*].

### Remarks
- *completely* rewriting authentication mechanism.
- *partially* rewriting authorization mechamism [keeping compatibility with ASPNET authorization policies mechanism].
- cookie authentication services *surgically* implement session-based cookies feature [using *IsSessionBasedCookie* func]. Authenticating, signingin and signingout services are completely independent each other [no dependencies on HttpContext features]. Authentication session-based cookies service is completely isolated.
- authentication options implementation contains only data [eg. *CookieAuthenticationOptions*]. Cookie authentication services [non DI-based ones] receive all dependencies as parameters.
- Microsoft ASPNET authentication options implementation contains data and behaviour/services [eg. *SessionStore*, *TicketDataFormat*, *SystemClock* for *CookieAuthenticationOptions*]. This design have some advantages comparing with my implementation allowing options:
  - to have different services from those registered on DI.
  - to encapsulate and carry on those services through the authentication flow [reducing the number of parameters so].
- *AuthenticateOAuth* oauth authentication func use template design pattern allowing oauth libraries to override when neccessary *postAuthenticate*, *exchangeCodeForTokens* or *accessUserInfo* funcs dependencies [eg. *AuthenticateTwitter*, *AuthenticateFacebook*].

[wip JWT, Sample.Api, Sample.www]
