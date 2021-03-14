using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShootingWebAgent.Models;
using ShootingWebAgent.Services;
using ShootingWebAgent.SQLite;

namespace ShootingWebAgent.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDataSingleton _dataSingleton;
        private readonly DataDbContext _context;
        
        public HomeController(ILogger<HomeController> logger, IDataSingleton dataSingleton, DataDbContext context)
        {
            _logger = logger;
            _dataSingleton = dataSingleton;
            _context = context;
        }

        #region Views

        public IActionResult Index()
        {
            var matches = _context.Matches.Include(m => m.Teams).ToList();
            matches.Sort((m1, m2) => m2.MatchId.CompareTo(m1.MatchId));
            return View(matches);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Match(int matchId)
        {
            _logger.LogDebug($"MatchId: {matchId}");

            if (_context.Matches.Count() > 0)
            {
                return View(_context.Matches.Single(m => m.MatchId.Equals(matchId)));
            }

            return View();
        }

        #endregion

        #region Actions

        public IActionResult GoToMatch(int? id)
        {
            return LocalRedirect($"/Home/Match?matchId={id}");
        }

        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
