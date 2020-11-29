using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShootingWebAgent.DataModels;
using ShootingWebAgent.DataModels.APIModel;
using ShootingWebAgent.Services;
using ShootingWebAgent.SQLite;

namespace ShootingWebAgent.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly ILogger<DataController> _logger;
        private readonly DataDbContext _context;
        private readonly IScopedStatisticService _statisticService;

        public DataController(ILogger<DataController> logger, DataDbContext context,
            IScopedStatisticService statisticService)
        {
            _logger = logger;
            _context = context;
            _statisticService = statisticService;
        }

        // POST api/data/disag
        [HttpPost("disag")]
        public async Task<ActionResult> Post([FromBody] ShClientJson value)
        {
            try
            {
                if (!_context.Matches.Any())
                {
                    var match = new Match()
                    {
                        MatchName = "MyMatchName",
                        SessionCount = 4,
                        ShotsPerSession = 10,
                        MatchStatus = MatchStatus.Open,
                        Teams = new List<Team>(),
                        DisagData = new List<DisagJson>(),
                        StatisticModels = new List<StatisticModel>()
                    };

                    await _context.Matches.AddAsync(match);
                    await _context.SaveChangesAsync();
                }

                if (!_context.Teams.Any(team => team.TeamHashId == value.TeamHashId))
                {
                    _context.Matches.Include(m => m.Teams).First().Teams.Add(new Team()
                    {
                        TeamName = value.DisagJson.Objects[0].Shooter.Club.Name,
                        TeamHashId = value.TeamHashId
                    });
                    await _context.SaveChangesAsync();
                    //return Conflict(new AnswerModel()
                    //{
                    //    Answer = "Invalid Hash Id"
                    //});
                }
                
                if (_context.Matches.Include(m => m.Teams)
                    .Single(m => m.Teams.Any(team => team.TeamHashId == value.TeamHashId))
                    .MatchStatus == MatchStatus.Closed)
                {
                    return Conflict(new AnswerModel()
                    {
                        Answer = "Match is already Close",
                        Data = MatchStatus.Closed.ToString()
                    });
                }
                
                await _statisticService.RefreshStatistic(value);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "api/data/disag post error");
                return Conflict();
            }

            return Ok();
        }
    }
}