using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ShootingWebAgent.DataModels.APIModel;
using ShootingWebAgent.DataModels.InternalModels;
using ShootingWebAgent.SQLite;

namespace ShootingWebAgent.Services
{
    public class ScopedStatisticService : IScopedStatisticService
    {
        private readonly DataDbContext _context;
        private readonly ILogger<ScopedStatisticService> _logger;
        private readonly IStatisticDataHandler _dataHandler;

        public ScopedStatisticService(DataDbContext context, ILogger<ScopedStatisticService> logger,
            IStatisticDataHandler dataHandler)
        {
            _context = context;
            _logger = logger;
            _dataHandler = dataHandler;
        }

        public async Task RefreshStatistic(int gameIndex, DisagJson disagJson)
        {
            try
            {
                var statisticModels = _dataHandler.GetStatistics(0);
                var statisticModel = statisticModels.SingleOrDefault(model =>
                    model.InternalId == disagJson.Objects.First().Shooter.InternalID);

                await _context.DisagJsons.AddAsync(disagJson);
                await _context.SaveChangesAsync();

                if (statisticModel != null)
                {
                    statisticModel.Count = disagJson.Objects.First().Count;
                    statisticModel.InternalCount++;
                    statisticModel.DecValue = disagJson.Objects.First().DecValue;
                    statisticModel.DecValueSum += disagJson.Objects.First().DecValue;
                    statisticModel.Points.Add(new Point()
                    {
                        x = disagJson.Objects.First().X,
                        y = disagJson.Objects.First().Y
                    });
                }
                else
                {
                    StatisticModel newStatisticModel = new StatisticModel()
                    {
                        InternalId = disagJson.Objects.First().Shooter.InternalID,
                        FirstName = disagJson.Objects.First().Shooter.Firstname,
                        LastName = disagJson.Objects.First().Shooter.Lastname,
                        InternalCount = 1,
                        Count = disagJson.Objects.First().Count,
                        DecValue = disagJson.Objects.First().DecValue,
                        DecValueSum = disagJson.Objects.First().DecValue,
                        Points = new List<Point>()
                        {
                            new Point()
                            {
                                x = disagJson.Objects.First().X,
                                y = disagJson.Objects.First().Y
                            }
                        }
                    };
                    statisticModels.Add(newStatisticModel);
                    _dataHandler.SaveStatisticModel(statisticModels);
                }
            }
            catch (Exception e)
            {
                _logger.LogError("RefreshStatistic Error!", e);
            }
        }
    }
}