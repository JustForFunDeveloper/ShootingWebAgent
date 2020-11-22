using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShootingWebAgent.Common;
using ShootingWebAgent.SQLite;

namespace ShootingWebAgent.Hub
{
    public class UpdateHub : Microsoft.AspNetCore.SignalR.Hub
    {
        private readonly ILogger<UpdateHub> _logger;
        private readonly DataDbContext _context;
        
        public UpdateHub(ILogger<UpdateHub> logger, DataDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task InitData()
        {
            if (_context.Matches.Any())
            {
                var match = _context.Matches
                    .Include(m => m.StatisticModels)
                    .ThenInclude(s => s.Points)
                    .Include(m => m.StatisticModels)
                    .ThenInclude(s => s.Sessions)
                    .First();
                await Clients.All.SendAsync("UpdateIndexPage", match.StatisticModels.ToJsonString());
            }
        }
        
        public DateTime GetCurrentTime()
        {
            return DateTime.UtcNow;
        }
    }
}