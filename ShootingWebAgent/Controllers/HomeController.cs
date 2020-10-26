using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShootingWebAgent.DataModels.APIModel;
using ShootingWebAgent.Models;
using ShootingWebAgent.Services;
using ShootingWebAgent.SQLite;

namespace ShootingWebAgent.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStatisticDataHandler _statisticDataHandler;
        
        public HomeController(ILogger<HomeController> logger, IStatisticDataHandler statisticDataHandler)
        {
            _logger = logger;
            _statisticDataHandler = statisticDataHandler;
        }

        public IActionResult Index()
        {
            return View(_statisticDataHandler.GetStatistics(0));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
