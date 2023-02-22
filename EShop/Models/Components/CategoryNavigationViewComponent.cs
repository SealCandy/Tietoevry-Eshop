using EShop.Models.DTOS;
using EShop.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EShop.Models.Components
{
    public class CategoryNavigationViewComponent : ViewComponent
    {
        public List<Category> categories;
        private static HttpClient _httpClient;
        private string URL = "https://eshopgunapi.azurewebsites.net/api/Categories";

        public CategoryNavigationViewComponent()
        {
            _httpClient = new HttpClient();
            GetCategory();
        }

        private List<Category> GetCategory()
        {
            categories = new List<Category>();
            var response = _httpClient.GetAsync(URL).Result;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                categories = JsonConvert.DeserializeObject<List<Category>>(response.Content.ReadAsStringAsync().Result);
            }
            return categories;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = categories;
            return await Task.FromResult((IViewComponentResult)View("CategoryNavigation", model));
        }
    }
}
