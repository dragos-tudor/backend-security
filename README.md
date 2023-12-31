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
- authentication libraries implement specific authentication services [eg. *AuthenticateCookie*, *SignInCookie*, *ChallengeGoogle*, *AuthenticateFacebookAsync*].
- authorization library implement authorization services [eg. *AuthorizeAsync*].

### Remarks
- rewriting completely authentication mechanism.
- rewriting partially authorization mechamism [keeping authorization policies mechanism].

[wip JWT, Sample.Api, Sample.www]
