using System.Threading.Tasks;
using ShootingWebAgent.DataModels.APIModel;

namespace ShootingWebAgent.Services
{
    public interface IScopedStatisticService
    {
        public Task RefreshStatistic(int gameIndex, DisagJson disagJson);
    }
}