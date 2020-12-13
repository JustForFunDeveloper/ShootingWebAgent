using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShootingWebAgent.Areas.Identity.Data;
using ShootingWebAgent.Data;
using ShootingWebAgent.DataModels;
using ShootingWebAgent.SQLite;

namespace ShootingWebAgent.Controllers
{
    public class MatchController : Controller
    {
        private readonly DataDbContext _context;
        private readonly IdentityContext _identityContext;
        private readonly UserManager<ShootingWebAgentUser> _userManager;
        private readonly ILogger<UserController> _logger;
        
        public class MatchInputModel : Match
        {
            [Required]
            [Display(Name = "Match Name")]
            public new string MatchName { get; set; }
            
            [Required]
            [Display(Name = "Anzahl der Serien")]
            public new int SessionCount { get; set; }
            
            [Required]
            [Display(Name = "Schuss pro Serie")]
            public new int ShotsPerSession { get; set; }
            
            [Required]
            [Display(Name = "Teams")]
            public List<TeamInput> TeamInputs { get; set; }

            public MatchInputModel()
            {
                TeamInputs = new List<TeamInput>()
                {
                    new TeamInput()
                };
            }
        }

        public MatchController(DataDbContext context, UserManager<ShootingWebAgentUser> userManager,
            ILogger<UserController> logger, IdentityContext identityContext)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
            _identityContext = identityContext;
        }

        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole(Roles.PremiumUser.ToString()))
                return Redirect("~/Home");

            var user = _userManager.GetUserAsync(HttpContext.User).Result;

            var matchIds = _identityContext.Users
                .Include(i => i.MatchIds)
                .Single(u => u.Id.Equals(user.Id))
                .MatchIds.Select(id => id.MatchId).ToList();

            var matches = _context.Matches
                .Include(m => m.Teams)
                .Where(m => matchIds.Contains(m.MatchId))
                .ToList();

            return View(matches);
        }

        public IActionResult Create()
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole(Roles.PremiumUser.ToString()))
                return Redirect("~/Home");
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MatchName,SessionCount,ShotsPerSession,TeamInputs")]
            MatchInputModel match)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(match);
        }
        
        public IActionResult Edit()
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole(Roles.PremiumUser.ToString()))
                return Redirect("~/Home");
            
            return View();
        }

        #region Actions

        public void EditMatch(int? id)
        {
            // return LocalRedirect($"/Home/Match?matchId={id}");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddOrderItem([Bind("TeamInputs")] MatchInputModel match)
        {
            match.TeamInputs.Add(new TeamInput());
            return PartialView("TeamInputs", match);
        }

        #endregion
    }
}