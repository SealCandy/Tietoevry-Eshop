using EShop.Models.DTOS;
using EShop.Models.Models;
using EShop.Models.Methods;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EShop.Controllers
{
    public class CategoryController:Controller
    {
        private static HttpClient _httpClient;
        private string URL = "https://eshopgunapi.azurewebsites.net/api/Categories/";

        static CategoryController()
        {
            _httpClient = new HttpClient();
        }
        public IActionResult Index(int id)
        {
            var response = _httpClient.GetAsync(URL + id).Result;
            NewCategoryDTO responseDTO = new NewCategoryDTO();
            if (response.StatusCode == System.Net.HttpStatusCode.OK) {

                 responseDTO = JsonConvert.DeserializeObject<NewCategoryDTO>(response.Content.ReadAsStringAsync().Result);
            }
            return View(responseDTO);
        }
    }
}