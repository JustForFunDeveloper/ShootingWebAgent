using System;
using System.Collections.Generic;

namespace ShootingWebAgent.DataModels
{
    public class StatisticModel
    {
        public int StatisticModelId { get; set; }
        
        public int Team { get; set; }
        public string TeamName { get; set; }

        public int Range { get; set; }

        public string InternalId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int Count { get; set; }
        public int InternalCount { get; set; }

        public double HR
        {
            get => Math.Round(_hr, 2);
            set => _hr = value;
        }
        private double _hr;
        public double DecValue
        {
            get => Math.Round(_decValue, 2);
            set => _decValue = value;
        }
        private double _decValue;
        public double DecValueSum
        {
            get => Math.Round(_decValueSum, 2);
            set => _decValueSum = value;
        }
        private double _decValueSum;

        public List<Point> Points { get; set; }
        public List<Session> Sessions { get; set; }
        public int SessionCount { get; set; }
        public int ShotsCount { get; set; }
    }

    public class Point
    {
        public int PointId { get; set; }
        public int x { get; set; }
        public int y { get; set; }
    }

    public class Session
    {
        public int SessionId { get; set; }
        public double value { get; set; }
    }
}