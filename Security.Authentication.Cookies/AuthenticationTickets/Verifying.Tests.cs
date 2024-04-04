using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.Cookies;

partial class CookiesTests
{
  [TestMethod]
  public void Non_slidable_expiration_ticket__verify_renewability_ticket__not_renewabled_ticket()
  {
    var ticket = CreateAuthenticationTicket(new ClaimsPrincipal(), default, string.Empty);
    Assert.IsFalse(IsRenewableAuthenticationTicket(ticket, default, false));
  }

  [TestMethod]
  public void Non_refreshable_ticket__verify_renewability_ticket__not_renewabled_ticket()
  {
    var authProperties = new AuthenticationProperties(){ AllowRefresh = false };
    var ticket = CreateAuthenticationTicket(new ClaimsPrincipal(), authProperties, string.Empty);
    Assert.IsFalse(IsRenewableAuthenticationTicket(ticket, default));
  }

  [TestMethod]
  public void Current_date_inside_first_half_ticket_validity_interval__verify_renewability_ticket__not_renewabled_ticket()
  {
    var currentUtc = DateTime.UtcNow;
    var authProperties = new AuthenticationProperties(){ AllowRefresh = true,
      IssuedUtc = currentUtc.AddMinutes(-1),
      ExpiresUtc = currentUtc.AddMinutes(2)
    };
    var ticket = CreateAuthenticationTicket(new ClaimsPrincipal(), authProperties, string.Empty);
    Assert.IsFalse(IsRenewableAuthenticationTicket(ticket, currentUtc));
  }

  [TestMethod]
  public void Current_date_inside_second_half_ticket_validity_interval__verify_renewability_ticket__renewabled_ticket()
  {
    var currentUtc = DateTime.UtcNow;
    var authProperties = new AuthenticationProperties(){ AllowRefresh = true,
      IssuedUtc = currentUtc.AddMinutes(-2),
      ExpiresUtc = currentUtc.AddMinutes(1)
    };
    var ticket = CreateAuthenticationTicket(new ClaimsPrincipal(), authProperties, string.Empty);
    Assert.IsTrue(IsRenewableAuthenticationTicket(ticket, currentUtc));
  }

  [TestMethod]
  public void Non_ticket_expires_date__verify_renewability_ticket__not_renewabled_ticket()
  {
    var currentUtc = DateTime.UtcNow;
    var authProperties = new AuthenticationProperties(){ AllowRefresh = true,
      IssuedUtc = currentUtc.AddMinutes(2)
    };
    var ticket = CreateAuthenticationTicket(new ClaimsPrincipal(), authProperties, string.Empty);
    Assert.IsFalse(IsRenewableAuthenticationTicket(ticket, currentUtc));
  }

  [TestMethod]
  public void Current_date_greater_than_ticket_expires_date__verify_expirability_ticket__expired_ticket()
  {
    var currentUtc = DateTime.UtcNow;
    var authProperties = new AuthenticationProperties(){ ExpiresUtc = currentUtc.AddMinutes(-1)};
    var ticket = CreateAuthenticationTicket(new ClaimsPrincipal(), authProperties, string.Empty);
    Assert.IsTrue(IsExpiredAuthenticationTicket(ticket, currentUtc));
  }

  [TestMethod]
  public void Non_ticket_expires_date__verify_expirability_ticket__not_expired_ticket()
  {
    var currentUtc = DateTime.UtcNow;
    var authProperties = new AuthenticationProperties(){ AllowRefresh = true,
      IssuedUtc = currentUtc.AddMinutes(2)
    };
    var ticket = CreateAuthenticationTicket(new ClaimsPrincipal(), authProperties, string.Empty);
    Assert.IsFalse(IsExpiredAuthenticationTicket(ticket, currentUtc));
  }
}