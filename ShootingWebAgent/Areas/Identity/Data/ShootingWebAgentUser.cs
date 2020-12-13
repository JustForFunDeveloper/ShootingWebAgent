using System;
using System.Collections.Generic;
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

        
        public DateTimeOffset? TrialEnd { get; set; }
        
        public int MaxMatchCount { get; set; }
        
        public ICollection<UserMatches> MatchIds { get; set; }
    }

    public enum Roles
    {
        Administrator,
        PremiumUser,
        TrialUser
    }

    public class UserMatches
    {
        public int Id { get; set; }
        public int MatchId { get; set; }
    }
}
