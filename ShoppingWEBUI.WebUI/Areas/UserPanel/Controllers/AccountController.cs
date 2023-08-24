using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using ShoppingWEBUI.Core.DTO;
using ShoppingWEBUI.Core.Result;
using ShoppingWEBUI.Core.ViewModel;
using System.Net;

namespace ShoppingWEBUI.WebUI.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    public class AccountController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<IdentityUser> _userManager;
        public AccountController(IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            
        }
        [HttpPost("/UserAccount/Register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.UserName };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    if (model.UserName == "ecmen" && model.ID == 1)
                    {
                        await _userManager.AddToRoleAsync(user, "admin");
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, "user");
                    }


                    var loginDTO = new LoginDTO
                    {
                        AdSoyad = model.UserName,
                        Sifre = model.Password,
                        UserID = model.ID,

                    };
                    return await Login(loginDTO);
                }
                else
                {

                    ModelState.AddModelError("", "Hesap oluşturma hatası.");
                }
            }

            return View(model);
        }

        [HttpGet("/User/Login")]
        public IActionResult Index()
        {
            _httpContextAccessor.HttpContext.Session.Clear();

            return View();
        }
        [HttpPost("/Account/UserLogin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var url = "http://localhost:5183/Login";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            var body = JsonConvert.SerializeObject(loginDTO);
            request.AddBody(body, "application/json");
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<ApiResult<LoginDTO>>(restResponse.Content);

            if (restResponse.StatusCode == HttpStatusCode.NotFound && responseObject?.Data == null)
            {
                ViewBag.LoginError = "Buraya Bir Data Geliyor";
                ViewData["LoginError"] = "Kullanıcı Adı Veya Şifre Yanlış";
                TempData["LoginError"] = "Buraya Başka Bir Data Geliyor";
                return View("Index");
            }
            else if (restResponse.StatusCode != HttpStatusCode.OK)
            {
                ViewData["LoginError"] = "Hata Oluştu";
                return View("Index");
            }

            _httpContextAccessor.HttpContext.Session.SetString("UserAdSoyad", responseObject.Data.AdSoyad);

            _httpContextAccessor.HttpContext.Session.SetString("UserID", responseObject.Data.UserID.ToString());

            return RedirectToAction("Index", "Home");
        }
    }
}
