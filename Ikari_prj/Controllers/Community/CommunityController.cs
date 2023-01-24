using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Ikari.Controllers.Community {
    public class CommunityController : BaseController {
        public CommunityController() : base("~/Views/Community/CommunityView.cshtml") { }
    }
}
