using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShootingWebAgent.Areas.Identity.Data;
using ShootingWebAgent.Common;
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
                TeamInputs = new List<TeamInput>();
            }

            public void AddTeamInputs()
            {
                TeamInputs?.Add(new TeamInput());
            }

            public void DeleteTeamInputs()
            {
                TeamInputs?.RemoveAt(0);
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

            var match = new MatchInputModel();
            match.AddTeamInputs();
            
            return View(match);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MatchName,SessionCount,ShotsPerSession,TeamInputs")]
            MatchInputModel match)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole(Roles.PremiumUser.ToString()))
                return Redirect("~/Home");

            if (ModelState.IsValid)
            {
                var newMatch = new Match()
                {
                    MatchName = match.MatchName,
                    SessionCount = match.SessionCount,
                    ShotsPerSession = match.ShotsPerSession,
                    MatchStatus = MatchStatus.Open,
                    Teams = new List<Team>(),
                    DisagData = new List<DisagJson>(),
                    StatisticModels = new List<StatisticModel>()
                };
                
                foreach (var teamInput in match.TeamInputs)
                {
                    newMatch.Teams.Add(new Team()
                    {
                        TeamName = teamInput.TeamName,
                        TeamHashId = teamInput.TeamName.GetMd5Hash()
                    });
                }
                
                await _context.Matches.AddAsync(newMatch);
                await _context.SaveChangesAsync();
                
                var user = _userManager.GetUserAsync(HttpContext.User).Result;

                var matchIds = _identityContext.Users
                    .Include(i => i.MatchIds)
                    .Single(u => u.Id.Equals(user.Id)).MatchIds;
                
                matchIds.Add(new UserMatches()
                {
                    MatchId = newMatch.MatchId
                });

                await _identityContext.SaveChangesAsync();
                
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
        public async Task<ActionResult> AddTeam([Bind("TeamInputs")] MatchInputModel match)
        {
            match.AddTeamInputs();
            return PartialView("TeamInputs", match);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteTeam([Bind("TeamInputs")] MatchInputModel match)
        {
            match.DeleteTeamInputs();
            // return PartialView("TeamInputs", match);
            return Ok();
        }

        #endregion
    }
}