using System.Security.Claims;
using System.Text.Encodings.Web;
using AlifTech.Interview.Ewallet.Extensions;
using AlifTech.Interview.Ewallet.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AlifTech.Interview.Ewallet.Auth;

public class DigestAuthenticationHandler : AuthenticationHandler<DigestAuthenticationOptions>
{
    private const string XUserId = "X-UserId";
    private const string XDigest = "X-Digest";

    private readonly IUserService _userService;
    private readonly IDigestGenerator _digestGenerator;

    public DigestAuthenticationHandler(
        IOptionsMonitor<DigestAuthenticationOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock, IUserService userService, IDigestGenerator digestGenerator)
        : base(options, logger, encoder, clock)
    {
        _userService = userService;
        _digestGenerator = digestGenerator;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var userId = Request.Headers[XUserId];
        var digest = Request.Headers[XDigest];

        if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(digest))
        {
            return AuthenticateResult.Fail("Missing X-UserId or X-Digest header");
        }

        var secretKey = await _userService.GetUserSecretKeyAsync(userId);
        if (string.IsNullOrEmpty(secretKey))
        {
            return AuthenticateResult.Fail("User not found");
        }

        var requestBody = await Request.GetRawBodyStringAsync();
        var jObject = JObject.Parse(requestBody);

        var json = JsonConvert.SerializeObject(jObject);

        var expectedDigest = await _digestGenerator.GenerateDigestAsync(message: json, secret: secretKey);

        if (digest != expectedDigest)
        {
            return AuthenticateResult.Fail("Invalid digest");
        }

        var claims = new[]
        {
            new Claim(DigestAuthenticationDefaults.UserIdClaimType, userId)
        };
        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return AuthenticateResult.Success(ticket);
    }
}