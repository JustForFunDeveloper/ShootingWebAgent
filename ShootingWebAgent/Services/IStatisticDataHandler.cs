using System.Collections.Generic;
using ShootingWebAgent.DataModels.InternalModels;

namespace ShootingWebAgent.Services
{
    public interface IStatisticDataHandler
    {
        public List<StatisticModel> GetStatistics(int gameIndex);
        public void SaveStatisticModel(List<StatisticModel> statisticList);
    }
}