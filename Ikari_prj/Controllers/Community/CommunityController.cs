using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Ikari.Controllers.Community {
    /// <summary>
    /// Контроллер сообщества
    /// </summary>
    public class CommunityController : BaseController {
        public CommunityController() : base("~/Views/Community/CommunityView.cshtml") { }
    }
}
