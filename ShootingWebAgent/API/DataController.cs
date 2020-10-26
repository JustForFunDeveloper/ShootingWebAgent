using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShootingWebAgent.Common;
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
        public async Task<ActionResult> Post([FromBody] DisagJson value)
        {
            try
            {
                _logger.LogInformation(value.ToJsonString());
                await _statisticService.RefreshStatistic(0, value);
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