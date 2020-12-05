using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using ShootingWebAgent.Areas.Identity.Data;

namespace ShootingWebAgent.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ShootingWebAgentUser> _userManager;

        [BindProperty] public RegisterModel.InputModel Input { get; set; }
        
        public UserController(UserManager<ShootingWebAgentUser> userManager)
        {
            _userManager = userManager;
        }

        // GET: UserController
        public ActionResult Index()
        {
            return View(_userManager.Users);
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return null;
            // return View();
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return null;
            // return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return null;
                // return View();
            }
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return null;
            // return View();
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return null;
                // return View();
            }
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return null;
            // return View();
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return null;
                // return View();
            }
        }
    }
}
