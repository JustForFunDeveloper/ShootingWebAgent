using System;

namespace ShootingWebAgent.DataModels.Users
{
    public class User
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }

        public DateTimeOffset? TrialEnd { get; set; }
    }
}