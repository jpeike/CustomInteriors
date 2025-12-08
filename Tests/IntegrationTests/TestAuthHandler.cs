using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Testing.IntegrationTests;

public class TestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public TestAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> o, ILoggerFactory l, UrlEncoder e, ISystemClock c)
        : base(o, l, e, c) {}

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        ClaimsIdentity identity = new ClaimsIdentity("IntegrationTestUser");
        identity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
        ClaimsPrincipal principal = new(identity);
        AuthenticationTicket ticket = new(principal, "IntegrationTestScheme");
        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}