using System.Collections.Generic;

namespace ShootingWebAgent.DataModels.InternalModels
{
    public class StatisticModel
    {
        public string InternalId;
        public string FirstName;
        public string LastName;
        public int Count;
        public int InternalCount;
        public double DecValue;
        public double DecValueSum;
        public List<Point> Points;
    }

    public class Point
    {
        public int x;
        public int y;
    }
}