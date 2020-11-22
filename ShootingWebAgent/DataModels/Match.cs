using System.Collections.Generic;

namespace ShootingWebAgent.DataModels
{
    public class Match
    {
        public int MatchId { get; set; }
        public string MatchName { get; set; }
        public int SessionCount { get; set; }
        public int ShotsPerSession { get; set; }
        public MatchStatus MatchStatus { get; set; }
        public ICollection<Team> Teams { get; set; }
        public ICollection<DisagJson> DisagData { get; set; }
        public ICollection<StatisticModel> StatisticModels { get; set; }
    }

    public class Team
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public string TeamHashId { get; set; }
    }

    public enum MatchStatus
    {
        Open,
        Closed
    }
}