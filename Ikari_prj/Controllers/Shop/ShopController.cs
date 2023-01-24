using Ikari.Data.Abstraction.Repositories;
using Microsoft.AspNetCore.Mvc;
using DevExtreme.AspNet.Data;
using Newtonsoft.Json;
using System.Security.Claims;
using Ikari.Data.Models.Entities.ShopItems;
using Ikari.Data.Models.ViewModels.DataGridModels;
using Ikari.Data.Abstraction;

namespace Ikari.Controllers.Shop
{
    public class ShopController : BaseController {
        ShopRepository _shopRepo;
        public ShopController(IkariDbContext context) : base("~/Views/Shop/ShopView.cshtml") {
            _shopRepo = new ShopRepository(context);
        }
        public override IActionResult GetPage() {
            return View(_path);
        }
        [HttpPost]
        public IActionResult GetSwords(DataGridLoadOptions options) {
            var resp = _shopRepo.GetSwords(options);

            return new JsonResult(resp, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
        }
        public IActionResult BuyItem(Guid id, string type) {
            var res = _shopRepo.BuyItem(id, CurrentUser.Id, type);
            return new JsonResult(res ? "Success!" : "Error occured!");
        }
    }
}
