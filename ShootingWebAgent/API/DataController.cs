using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ShootingWebAgent.DataModels.APIModel;
using ShootingWebAgent.SQLite;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShootingWebAgent.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly ILogger<DataController> _logger;
        private readonly DataDbContext _context;
        
        public DataController(ILogger<DataController> logger, DataDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // POST api/data/disag
        [HttpPost("disag")]
        public async Task<ActionResult> Post([FromBody] DisagJson value)
        {
            _logger.LogInformation(JsonConvert.SerializeObject(value));
            await _context.DisagJsons.AddAsync(value);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
