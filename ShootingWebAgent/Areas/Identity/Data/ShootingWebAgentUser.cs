using System;
using Microsoft.AspNetCore.Identity;

namespace ShootingWebAgent.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the ShootingWebAgentUser class
    public class ShootingWebAgentUser : IdentityUser
    {
        public override string Id { get; set; }
        public override string Email { get; set; }
        public override bool EmailConfirmed { get; set; }
        public override DateTimeOffset? LockoutEnd { get; set; }
        public override bool LockoutEnabled { get; set; }

        // public DateTimeOffset? TrialEnd { get; set; }
    }

    public enum Roles
    {
        Administrator,
        PremiumUser,
        TrialUser
    }
}
