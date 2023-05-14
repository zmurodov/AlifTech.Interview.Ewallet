using Microsoft.AspNetCore.Authentication;

namespace AlifTech.Interview.Ewallet.Auth;

public static class AuthenticationBuilderExtensions
{
    /// <summary>
    /// Enables digest authentication
    /// </summary>
    /// <param name="builder">The <see cref="AuthenticationBuilder"/></param>
    /// <returns>A reference to builder after the operation has completed.</returns>
    public static AuthenticationBuilder AddDigestAuthentication(this AuthenticationBuilder builder) =>
        builder.AddDigestAuthentication(DigestAuthenticationDefaults.AuthenticationScheme, DigestAuthenticationDefaults.AuthenticationScheme);

    /// <summary>
    /// Enables digest authentication
    /// </summary>
    /// <param name="builder">The <see cref="AuthenticationBuilder"/></param>
    /// <param name="authenticationScheme">The authentication scheme.</param>
    /// <param name="displayName">The display name for the authentication handler.</param>
    /// <returns>A reference to builder after the operation has completed.</returns>        
    public static AuthenticationBuilder AddDigestAuthentication(this AuthenticationBuilder builder,
        string authenticationScheme,
        string displayName)
    {
        return builder.AddScheme<DigestAuthenticationOptions, DigestAuthenticationHandler>(authenticationScheme,
            displayName, options => { });
    }
}