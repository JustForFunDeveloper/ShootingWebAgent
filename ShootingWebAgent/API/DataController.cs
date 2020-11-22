using System;
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
                if (!_context.Teams.Any(team => team.TeamHashId == value.TeamHashId))
                {
                    return Conflict(new AnswerModel()
                    {
                        Answer = "Invalid Hash Id"
                    });
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
                _logger.LogError("api/data/disag post error", e);
                return Conflict();
            }

            return Ok();
        }
    }
}