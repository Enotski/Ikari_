using Ikari.Data.Abstraction.Repositories;
using Ikari.Data.Models.Entities.UserProfile;
using Ikari.Data.Models.ViewModels.UserProfile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Ikari.Controllers
{
    /// <summary>
    /// Базовый контроллер
    /// </summary>
    public class BaseController : Controller {
        protected readonly string _path;
        public static UserViewModel? CurrentUser { get; set; }
        public BaseController(string path) {
            _path = path;
        }
        [Authorize]
        public virtual IActionResult GetPage() {
            return View(_path);
        }
    }
}
