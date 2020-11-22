using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ShootingWebAgent.Hub
{
    public class UpdateHub : Microsoft.AspNetCore.SignalR.Hub
    {
        private readonly ILogger<UpdateHub> _logger;
        
        public UpdateHub(ILogger<UpdateHub> logger)
        {
            _logger = logger;
        }

        public async Task InitData()
        {
           _logger.LogError("Got here!");
        }
        
        public DateTime GetCurrentTime()
        {
            return DateTime.UtcNow;
        }
    }
}