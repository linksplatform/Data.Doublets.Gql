using Microsoft.AspNetCore.Authorization;

namespace Platform.Data.Doublets.Gql.Server
{
    /// <summary>
    ///     A <see cref="IAuthorizationRequirement" /> enforcing a minimum user age.
    ///     (sample taken from https://docs.microsoft.com/en-us/aspnet/core/security/authorization/policies)
    /// </summary>
    public class MinimumAgeRequirement : IAuthorizationRequirement
    {
        public MinimumAgeRequirement(int minimumAge) => MinimumAge = minimumAge;
        public int MinimumAge { get; }
    }
}
