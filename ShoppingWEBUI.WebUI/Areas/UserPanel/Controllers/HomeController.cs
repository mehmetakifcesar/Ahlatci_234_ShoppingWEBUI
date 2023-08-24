using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingWEBUI.WebUI.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    public class HomeController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpGet("/User/Anasayfa")]
        [Authorize]
        public IActionResult Index()
        {
            if (User.IsInRole("admin"))
            {
                return View("Admin/Anasayfa");
            }
            else
            {
                return View("User/Anasayfa");
            }
        }
    }
}
