using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using ShootingWebAgent.Areas.Identity.Data;
using ShootingWebAgent.Hub;

namespace ShootingWebAgent.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ShootingWebAgentUser> _userManager;
        private readonly ILogger<UserController> _logger;
        private readonly IHubContext<UpdateHub> _hubContext;

        public class InputModel
        {
            public string Id { get; set; }
            
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
                MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
            
            [Display(Name = "Lockout Enabled")]
            public bool LockoutEnabled { get; set; }
            
            [Display(Name = "Lockout End")]
            public DateTimeOffset? LockoutEnd { get; set; }
            
            [Display(Name = "Email Confirmed")]
            public bool EmailConfirmed { get; set; }

            [Display(Name = "Administrator")]
            public bool IsAdministrator { get; set; }
            
            [Display(Name = "PremiumUser")]
            public bool IsPremiumUser { get; set; }
            
            [Display(Name = "TrialUser")]
            public bool IsTrialUser { get; set; }
        }
        
        public class EditModel : InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public new string Email { get; set; }
            
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
                MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Old Password")]
            public string OldPassword { get; set; }
            
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
                MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "New Password")]
            public new string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm new password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public new string ConfirmPassword { get; set; }
        }

        public UserController(UserManager<ShootingWebAgentUser> userManager, ILogger<UserController> logger, IHubContext<UpdateHub> hubContext)
        {
            _userManager = userManager;
            _logger = logger;
            _hubContext = hubContext;
        }

        // GET: UserController
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole(Roles.Administrator.ToString()))
                return LocalRedirect("/Identity/Account/Login");

            var userList = new List<InputModel>();
            foreach (var user in _userManager.Users)
            {
                userList.Add(GetInputModelFromUser(user));
            }
            
            return View(userList);
        }

        // GET: UserController/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (!User.Identity.IsAuthenticated || !User.IsInRole(Roles.Administrator.ToString()))
                return LocalRedirect("/Identity/Account/Login");

            var user = _userManager.Users.Single(u => u.Id.Equals(id));
            
            return View(GetInputModelFromUser(user));
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole(Roles.Administrator.ToString()))
                return LocalRedirect("/Identity/Account/Login");

            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Email,Password,ConfirmPassword,LockoutEnabled,EmailConfirmed,IsAdministrator,IsPremiumUser,IsTrialUser")]
            InputModel inputModel)
        {
            try
            {
                var user = new ShootingWebAgentUser
                {
                    UserName = inputModel.Email,
                    Email = inputModel.Email,
                    LockoutEnabled = inputModel.LockoutEnabled,
                    EmailConfirmed = inputModel.EmailConfirmed
                };
                var result = await _userManager.CreateAsync(user, inputModel.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                }
                else
                {
                    _logger.LogError($"Couldn't create user {user.Id}");
                    // return Redirect("~/Home/Error");
                }
                
                IdentityResult addRoleResult;
                if (inputModel.IsAdministrator)
                {
                    addRoleResult = await _userManager.AddToRoleAsync(user, Roles.Administrator.ToString());
                    if (addRoleResult.Succeeded)
                    {
                        _logger.LogInformation($"{Roles.Administrator} Role added to {user.UserName}.");
                    }
                }
                if (inputModel.IsPremiumUser)
                {
                    addRoleResult = await _userManager.AddToRoleAsync(user, Roles.PremiumUser.ToString());
                    if (addRoleResult.Succeeded)
                    {
                        _logger.LogInformation($"{Roles.TrialUser} Role added to {user.UserName}.");
                    }
                }
                if (inputModel.IsTrialUser)
                {
                    addRoleResult = await _userManager.AddToRoleAsync(user, Roles.TrialUser.ToString());
                    if (addRoleResult.Succeeded)
                    {
                        _logger.LogInformation($"{Roles.TrialUser} Role added to {user.UserName}.");
                    }
                }
                
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(string id)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole(Roles.Administrator.ToString()))
                return LocalRedirect("/Identity/Account/Login");

            var user = _userManager.Users.Single(u => u.Id.Equals(id));
            
            return View(GetEditModelFromUser(user));
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, [Bind("Email,Password,ConfirmPassword,LockoutEnabled,EmailConfirmed,IsAdministrator,IsPremiumUser,IsTrialUser")]
            EditModel editModel)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);

                if (editModel.Email?.Length > 0)
                {
                    user.Email = editModel.Email;
                    user.UserName = editModel.Email;
                }

                try
                {
                    if (editModel.Password?.Length > 0)
                    {
                        await _userManager.ChangePasswordAsync(user, editModel.OldPassword, editModel.Password);
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError("Password", "Couldn't change Password!");
                    throw;
                }

                user.LockoutEnabled = editModel.LockoutEnabled;
                user.EmailConfirmed = editModel.EmailConfirmed;

                var result = await _userManager.UpdateAsync(user);
                
                if (result.Succeeded)
                {
                    _logger.LogInformation("Changed user credentials");
                }
                else
                {
                    _logger.LogError($"Couldn't change user {user.Id}");
                    return Redirect("~/Home/Error");
                }

                if (!editModel.IsAdministrator.Equals(_userManager.IsInRoleAsync(user,Roles.Administrator.ToString()).Result))
                {
                    if (editModel.IsAdministrator)
                    {
                        await _userManager.AddToRoleAsync(user, Roles.Administrator.ToString());
                    }
                    else
                    {
                        await _userManager.RemoveFromRoleAsync(user, Roles.Administrator.ToString());
                    }
                }
                if (!editModel.IsPremiumUser.Equals(_userManager.IsInRoleAsync(user,Roles.PremiumUser.ToString()).Result))
                {
                    if (editModel.IsPremiumUser)
                    {
                        await _userManager.AddToRoleAsync(user, Roles.PremiumUser.ToString());
                    }
                    else
                    {
                        await _userManager.RemoveFromRoleAsync(user, Roles.PremiumUser.ToString());
                    }
                }
                if (!editModel.IsTrialUser.Equals(_userManager.IsInRoleAsync(user,Roles.TrialUser.ToString()).Result))
                {
                    if (editModel.IsTrialUser)
                    {
                        await _userManager.AddToRoleAsync(user, Roles.TrialUser.ToString());
                    }
                    else
                    {
                        await _userManager.RemoveFromRoleAsync(user, Roles.TrialUser.ToString());
                    }
                }
                
                IdentityResult addRoleResult;
                if (editModel.IsAdministrator)
                {
                    addRoleResult = await _userManager.AddToRoleAsync(user, Roles.Administrator.ToString());
                    if (addRoleResult.Succeeded)
                    {
                        _logger.LogInformation($"{Roles.Administrator} Role added to {user.UserName}.");
                    }
                }
                if (editModel.IsPremiumUser)
                {
                    addRoleResult = await _userManager.AddToRoleAsync(user, Roles.PremiumUser.ToString());
                    if (addRoleResult.Succeeded)
                    {
                        _logger.LogInformation($"{Roles.TrialUser} Role added to {user.UserName}.");
                    }
                }
                if (editModel.IsTrialUser)
                {
                    addRoleResult = await _userManager.AddToRoleAsync(user, Roles.TrialUser.ToString());
                    if (addRoleResult.Succeeded)
                    {
                        _logger.LogInformation($"{Roles.TrialUser} Role added to {user.UserName}.");
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(editModel);
            }
        }

        private InputModel GetInputModelFromUser(ShootingWebAgentUser user)
        {
            return new InputModel()
            {
                Id = user.Id,
                Email = user.Email,
                LockoutEnabled = user.LockoutEnabled,
                EmailConfirmed = user.EmailConfirmed,
                IsAdministrator = _userManager.IsInRoleAsync(user, Roles.Administrator.ToString()).Result,
                IsPremiumUser = _userManager.IsInRoleAsync(user, Roles.PremiumUser.ToString()).Result,
                IsTrialUser = _userManager.IsInRoleAsync(user, Roles.TrialUser.ToString()).Result,
            };
        }
        
        private EditModel GetEditModelFromUser(ShootingWebAgentUser user)
        {
            return new EditModel()
            {
                Id = user.Id,
                Email = user.Email,
                LockoutEnabled = user.LockoutEnabled,
                EmailConfirmed = user.EmailConfirmed,
                IsAdministrator = _userManager.IsInRoleAsync(user, Roles.Administrator.ToString()).Result,
                IsPremiumUser = _userManager.IsInRoleAsync(user, Roles.PremiumUser.ToString()).Result,
                IsTrialUser = _userManager.IsInRoleAsync(user, Roles.TrialUser.ToString()).Result,
            };
        }
    }
}