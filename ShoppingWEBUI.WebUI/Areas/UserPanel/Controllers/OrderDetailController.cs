using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using ShoppingWEBUI.Core.DTO;
using ShoppingWEBUI.Core.Result;
using ShoppingWEBUI.Core.ViewModel;
using ShoppingWEBUI.Helper.SessionHelper;
using System.Net;

namespace ShoppingWEBUI.WebUI.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize(Roles = "user")]
    public class OrderDetailController : Controller
    {
       private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderDetailController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpGet("/User/OrderDetail")]
        public async Task<IActionResult> Index()
        {
            OrderViewModel orderDetailViewModel = new OrderViewModel()
            {
                Categories = await GetCategoriList(),
                Products = await GetProductList(),
                Orders = await GetOrderList(),
                OrderDetails = await GetOrderDetailList()

               
            };
            return View(orderDetailViewModel);
        }

        

        [HttpPost("/User/AddOrderDetail")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrderDetail(OrderDetailDTO orderDetailDTO)
        {
            var url = "http://localhost:5183/AddOrderDetail";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            var body = JsonConvert.SerializeObject(orderDetailDTO);
            request.AddBody(body, "application/json");
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<ApiResult<bool>>(restResponse.Content);

            if (restResponse.StatusCode == HttpStatusCode.OK)
            {
                return Json(new { success = true, data = responseObject.Data });
            }
            else if (restResponse.StatusCode == HttpStatusCode.BadRequest)
            {
                return Json(new { success = false, HataBilgisi = responseObject.HataBilgisi });
            }
            else
            {
                return Json(new { success = false, HataBilgisi = responseObject.HataBilgisi });
            }
        }
        [HttpPost("/User/Order{orderDetailGuid")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetOrderDetail(Guid orderDetailGuid)
        {
            var url = "http://localhost:5183/OrderDetail" + orderDetailGuid;
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            var body = JsonConvert.SerializeObject(orderDetailGuid);
            request.AddBody(body, "application/json");
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<ApiResult<OrderDetailDTO>>(restResponse.Content);
            if (restResponse.StatusCode == HttpStatusCode.OK)
            {
                return Json(new { success = true, data = responseObject.Data });
            }
            else if (restResponse.StatusCode == HttpStatusCode.BadRequest)
            {
                return Json(new { success = false, HataBilgisi = responseObject.HataBilgisi });
            }
            else
            {
                return Json(new { success = false, HataBilgisi = responseObject.HataBilgisi });
            }
        }
        [HttpPost("/User/UpdateOrderDetail")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateOrderDetail(OrderDetailDTO orderDetailDTO)
        {
            var url = "http://locahost:5183/UpdateOrderDetail";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Put);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            var body = JsonConvert.SerializeObject(orderDetailDTO);
            request.AddBody(body, "application/json");
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<ApiResult<bool>>(restResponse.Content);
            if (restResponse.StatusCode == HttpStatusCode.OK)
            {
                return Json(new { success = true, data = responseObject.Data });
            }
            else if (restResponse.StatusCode == HttpStatusCode.BadRequest)
            {
                return Json(new { success = false, HataBilgisi = responseObject.HataBilgisi });
            }
            else
            {
                return Json(new { success = false, HataBilgisi = responseObject.HataBilgisi });
            }
        }
        [HttpPost("/User/RemoveOrder/{orderDetailGuid}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveOrderDetail(Guid orderDetailGuid)
        {
            var url = "http://localhost:5183/RemoveOrderDetail" + orderDetailGuid;
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Delete);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<ApiResult<bool>>(restResponse.Content);

            if (restResponse.StatusCode == HttpStatusCode.OK)
            {
                return Json(new { success = true, data = responseObject.Data });
            }
            else if (restResponse.StatusCode == HttpStatusCode.BadRequest)
            {
                return Json(new { success = false, HataBilgisi = responseObject.HataBilgisi });
            }
            else
            {
                return Json(new { success = false, HataBilgisi = responseObject.HataBilgisi });
            }

        }

        public async Task<List<OrderDTO>> GetOrderList()
        {

            var url = "http://localhost:5183/Orders";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<ApiResult<List<OrderDTO>>>(restResponse.Content);

            var orders = responseObject.Data;

            return orders;

        }

        public async Task<List<OrderDetailDTO>> GetOrderDetailList()
        {
            var url = "http://localhost:5183/OrderDetails";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<ApiResult<List<OrderDetailDTO>>>(restResponse.Content);

            var orderDetails = responseObject.Data;

            return orderDetails;
        }

        public async Task<List<ProductDTO>> GetProductList()
        {
            var url = "http://localhost:5183/Products";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<ApiResult<List<ProductDTO>>>(restResponse.Content);

            var products = responseObject.Data;

            return products;
        }

        public async Task<List<CategoryDTO>> GetCategoriList()
        {
            var url = "http://localhost:5183/Categories";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<ApiResult<List<CategoryDTO>>>(restResponse.Content);

            var categories = responseObject.Data;

            return categories;
        }
    }
}
