using System.Collections.Generic;
using ShootingWebAgent.DataModels.InternalModels;

namespace ShootingWebAgent.Services
{
    public class StatisticDataHandler : IStatisticDataHandler
    {
        private List<StatisticModel> _statisticList = new List<StatisticModel>();

        public List<StatisticModel> GetStatistics(int gameIndex)
        {
            return _statisticList;
        }

        public void SaveStatisticModel(List<StatisticModel> statisticList)
        {
            _statisticList = statisticList;
        }
    }
}