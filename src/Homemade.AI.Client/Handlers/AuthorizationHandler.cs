using System.Net.Http.Headers;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Homemade.AI.Client.Handlers;

/// <summary>
/// Attaches the access token from the current user to the outgoing request.
/// </summary>
/// <param name="httpContextAccessor"></param>
internal sealed class AuthorizationHandler(
    IHttpContextAccessor httpContextAccessor
) : DelegatingHandler
{
    /// <inheritdoc />
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var httpContext = httpContextAccessor.HttpContext ??
                          throw new InvalidOperationException("""
                                                              No HttpContext available from the IHttpContextAccessor.
                                                              """);

        var accessToken = await httpContext.GetTokenAsync("access_token");

        if (!string.IsNullOrWhiteSpace(accessToken))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}