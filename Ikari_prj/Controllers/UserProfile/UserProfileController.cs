using Ikari.Data.Abstraction.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using System.Linq;
using Ikari.Data.Models.ViewModels.UserProfile;
using Ikari.Data.Abstraction;

namespace Ikari.Controllers.UserProfile {

    /// <summary>
    /// Контроллер профиля пользователя
    /// </summary>
    public class UserProfileController : BaseController {
        readonly UserProfileRepository _userRepo;
        public UserProfileController(IkariDbContext context) : base("~/Views/UserProfile/UserProfileView.cshtml") {
            _userRepo = new UserProfileRepository(context);
        }
        public IActionResult GetLoginPage() {
            return View("~/Views/UserProfile/LoginView.cshtml");
        }
        public IActionResult Login(string? name, string? password) {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(password))
                return GetLoginPage();

            var user = _userRepo.GetUserViewModel(name, password);
            if(user == null) {
                return GetLoginPage();
            }
            CurrentUser = user;
            var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) };
            // создаем объект ClaimsIdentity
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            // установка аутентификационных куки
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return Redirect("~/Home/GetPage");
        }
        public IActionResult Register(string? name, string? email, string? password) {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(password))
                return Content("<script>alert('Fill all fields')</script>");

            UserViewModel newUser = _userRepo.RegisterNewUser(name, email, password);
            CurrentUser = newUser;

            var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, newUser.Id.ToString()) };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return Redirect("~/Home/GetPage");
        }
        public IActionResult Logout() {
            CurrentUser = null;
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return GetLoginPage();
        }
        [HttpPost]
        public IActionResult GetUserInfo() {
            if (CurrentUser == null || CurrentUser.Id == Guid.Empty) {
                var userKey = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                var parsed = Guid.TryParse(userKey, out Guid userId);
                if (parsed && userId != Guid.Empty) {
                    CurrentUser = _userRepo.GetUserViewModel(userId);

                    //if (CurrentUser == null)
                    //   return RedirectToAction("GetLoginPage");
                } else {
                    //return RedirectToAction("GetLoginPage");
                }
            }
            var resp = JsonConvert.SerializeObject(CurrentUser, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            return new JsonResult(resp);
        }
    }
}
