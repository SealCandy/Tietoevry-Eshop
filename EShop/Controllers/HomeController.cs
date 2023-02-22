using EShop.Models.DTOS;
using EShop.Models.Models;
using EShop.Models;
using EShop.Models.Methods;
using EShop.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace EShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private string URL = "https://eshopgunapi.azurewebsites.net/api/Products";
        private static HttpClient _httpClient;

        static HomeController()
        {
          _httpClient = new HttpClient();
        }
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ShowProductVariantViewModel showProductVariantViewModel = new ShowProductVariantViewModel();
            var response = _httpClient.GetAsync(URL + "?Type=MOSTBOUGHT&Page=0&PageSize=3").Result;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                showProductVariantViewModel.mostBoughtProductVariants = JsonConvert.DeserializeObject<List<ProductVariantDTO>>(response.Content.ReadAsStringAsync().Result);
            }
            response = _httpClient.GetAsync(URL + "?Type=PRICE&Page=0&PageSize=3").Result;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                showProductVariantViewModel.discountedProductsVariants = JsonConvert.DeserializeObject<List<ProductVariantDTO>>(response.Content.ReadAsStringAsync().Result);
            }
            return View(showProductVariantViewModel);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
