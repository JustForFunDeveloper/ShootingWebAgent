using System.Threading.Tasks;
using ShootingWebAgent.DataModels.APIModel;

namespace ShootingWebAgent.Services
{
    public interface IScopedStatisticService
    {
        public Task RefreshStatistic(ShClientJson shClientJson);
    }
}