using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using ShootingWebAgent.Common;
using ShootingWebAgent.DataModels;
using ShootingWebAgent.DataModels.APIModel;
using ShootingWebAgent.Hub;
using ShootingWebAgent.SQLite;

namespace ShootingWebAgent.Services
{
    public class ScopedStatisticService : IScopedStatisticService
    {
        private readonly DataDbContext _context;
        private readonly ILogger<ScopedStatisticService> _logger;
        private readonly IStatisticDataHandler _dataHandler;
        private readonly IHubContext<UpdateHub> _hubContext;

        public ScopedStatisticService(DataDbContext context, ILogger<ScopedStatisticService> logger,
            IStatisticDataHandler dataHandler, IHubContext<UpdateHub> hubContext)
        {
            _context = context;
            _logger = logger;
            _dataHandler = dataHandler;
            _hubContext = hubContext;
        }

        public async Task RefreshStatistic(ShClientJson shClientJson)
        {
            try
            {
                var disagJson = shClientJson.DisagJson;
                var match = _context.Matches
                    .Include(m => m.Teams)
                    .Include(m => m.DisagData)
                    .Include(m => m.StatisticModels)
                    .ThenInclude(statistics => statistics.Points)
                    .Include(m => m.StatisticModels)
                    .ThenInclude(statistics => statistics.Sessions)
                    .Single(m => m.Teams.Any(t => t.TeamHashId == shClientJson.TeamHashId));
                
                match.DisagData.Add(shClientJson.DisagJson);
                
                Team team = match.Teams.Single(t => t.TeamHashId == shClientJson.TeamHashId);

                var statisticModel = match.StatisticModels.SingleOrDefault(model =>
                    model.InternalId == disagJson.Objects.First().Shooter.InternalID);

                if (statisticModel != null)
                {
                    statisticModel.Count = disagJson.Objects.First().Count;
                    statisticModel.InternalCount++;

                    statisticModel.DecValue = Math.Round(disagJson.Objects.First().DecValue, 1);
                    statisticModel.DecValueSum += Math.Round(disagJson.Objects.First().DecValue, 1);
                    
                    double average = statisticModel.DecValueSum / statisticModel.InternalCount;
                    statisticModel.HR = Math.Round(average * match.ShotsPerSession * match.SessionCount, 1);

                    if (statisticModel.Points.Count % match.ShotsPerSession == 0)
                    {
                        statisticModel.Points.Clear();
                    }
                    statisticModel.Points.Add(new Point()
                    {
                        x = disagJson.Objects.First().X,
                        y = disagJson.Objects.First().Y
                    });
                    
                    statisticModel.Sessions.Last().value = Math.Round(statisticModel.Sessions.Last().value + statisticModel.DecValue, 1);
                    
                    if (statisticModel.Points.Count % match.ShotsPerSession == 0)
                    {
                        statisticModel.Sessions.Add(new Session()
                        {
                            value = Math.Round(0.0, 1)
                        });
                    }
                }
                else
                {
                    StatisticModel newStatisticModel = new StatisticModel()
                    {
                        Team = match.Teams.IndexOf(team) + 1,
                        TeamName = team.TeamName,
                        
                        Range = disagJson.Objects.First().Range,
                        
                        InternalId = disagJson.Objects.First().Shooter.InternalID,
                        FirstName = disagJson.Objects.First().Shooter.Firstname,
                        LastName = disagJson.Objects.First().Shooter.Lastname,
                        
                        Count = disagJson.Objects.First().Count,
                        InternalCount = 1,
                        
                        HR = Math.Round(disagJson.Objects.First().DecValue * match.ShotsPerSession * match.SessionCount, 1),
                        DecValue = Math.Round(disagJson.Objects.First().DecValue, 1),
                        DecValueSum = Math.Round(disagJson.Objects.First().DecValue, 1),
                        Points = new List<Point>()
                        {
                            new Point()
                            {
                                x = disagJson.Objects.First().X,
                                y = disagJson.Objects.First().Y
                            }
                        },
                        Sessions = new List<Session>()
                        {
                            new Session()
                            {
                                value = Math.Round(disagJson.Objects.First().DecValue, 1)
                            }
                        },
                        SessionCount = match.SessionCount,
                        ShotsCount = match.SessionCount * match.ShotsPerSession
                    };
                    match.StatisticModels.Add(newStatisticModel);
                }
                await _context.SaveChangesAsync();
                await _hubContext.Clients.All.SendAsync("UpdateIndexPage", match.StatisticModels.ToJsonString());
            }
            catch (Exception e)
            {
                _logger.LogError(e,"RefreshStatistic Error!");
            }
        }
    }
}