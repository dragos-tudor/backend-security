## ASPNET Security libraries
- simplified ASPNET Security-like libraries.
- functional-style libraries [90% OOP-free].

### Security libraries
- [Authentication.Cookies](/Security.Authentication.Cookies/).
- [Authentication.Facebook](/Security.Authentication.Facebook/).
- [Authentication.Google](/Security.Authentication.Google/).
- [Authentication.Twitter](/Security.Authentication.Twitter/).
- [Authorization](/Security.Authorization//).

### Design
- security libraries implement **authentication and authorization services** - the security backbone. These services are named **high-level** functions because act as security behaviour controllers and use **low-level** functions.
- security libraries were written following **FP principles** [pure functions, high-order functions, immutability, high-level functions with side-effects, separate data from functions/behaviour].
- DI is used as **thin layer** usually over functional security services [eg. *SignInCookie* have 2 implementations with/without DI services].
- **high-level** functions usually use declarative style [eg. *SignInCookie*].
- **low-level** functions usually use imperative style and are one-liners [eg. *IsSecuredCookie*].

### Services
- authentication libraries implement specific authentication services [**AuthenticateCookie**, **SignInCookie**, **ChallengeGoogle**, **AuthenticateFacebookAsync**].
- authorization library implement authorization services [**AuthorizeAsync**].

### Remarks
- rewriting completely authentication mechanism.
- rewriting partially authorization mechamism [keeping authorization policies mechanism].

[wip JWT, Sample.Api, Sample.www]
