using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShootingWebAgent.Areas.Identity.Data;
using ShootingWebAgent.Common;
using ShootingWebAgent.Services;
using ShootingWebAgent.SQLite;

namespace ShootingWebAgent.Hub
{
    public class UpdateHub : Microsoft.AspNetCore.SignalR.Hub
    {
        private readonly ILogger<UpdateHub> _logger;
        private readonly DataDbContext _context;
        private readonly SignInManager<ShootingWebAgentUser> _signInManager;
        private readonly UserManager<ShootingWebAgentUser> _userManager;
        private readonly IDataSingleton _dataSingleton;

        public UpdateHub(ILogger<UpdateHub> logger, DataDbContext context,
            SignInManager<ShootingWebAgentUser> signInManager, UserManager<ShootingWebAgentUser> userManager,
            IDataSingleton dataSingleton)
        {
            _logger = logger;
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _dataSingleton = dataSingleton;
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            int matchId = _dataSingleton.GetMatchFromGroupsDictionary(Context.ConnectionId);
            if (matchId < 0)
                return base.OnDisconnectedAsync(exception);
            
            Groups.RemoveFromGroupAsync(Context.ConnectionId, matchId.ToString());
            return base.OnDisconnectedAsync(exception);
        }

        public async Task InitData(string matchId)
        {
            try
            {
                int matchIdInt = int.Parse(matchId);

                await Groups.AddToGroupAsync(Context.ConnectionId, matchId);
                _dataSingleton.AddUserToGroupsDictionary(Context.ConnectionId, matchIdInt);
                
                if (_context.Matches.Any())
                {
                    var match = _context.Matches
                        .Include(m => m.StatisticModels)
                        .ThenInclude(s => s.Points)
                        .Include(m => m.StatisticModels)
                        .ThenInclude(s => s.Sessions)
                        .Single(m => m.MatchId.Equals(matchIdInt));
                    await Clients.Caller.SendAsync("UpdateIndexPage", match.StatisticModels.ToJsonString());
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e,"InitData Error");
            }
        }

        public async Task DeleteUser(string userId)
        {
            try
            {
                if (_signInManager.IsSignedIn(Context.User) && Context.User.IsInRole(Roles.Administrator.ToString()))
                {
                    var user = _userManager.Users.Single(u => u.Id.Equals(userId));
                    await _userManager.DeleteAsync(user);
                    _logger.LogDebug($"Delete User: {userId}");
                    await Clients.Caller.SendAsync("Refresh");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e,"DeleteUser Error");
            }
        }

        public DateTime GetCurrentTime()
        {
            return DateTime.UtcNow;
        }
    }
}