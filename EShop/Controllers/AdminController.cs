using EShop.Models.DTOS;
using EShop.Models.Models;
using EShop.Models.Methods;
using EShop.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.Dynamic;

namespace EShop.Controllers
{
    public class AdminController : Controller
    {
        private static HttpClient _httpClient;
        private string URL = "https://eshopgunapi.azurewebsites.net/api/Categories";

        static AdminController()
        {
            _httpClient = new HttpClient();
        }
        public List<Category> GetCategories()
        {
            List<Category> categories = null;
            var response = _httpClient.GetAsync(URL).Result;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                categories = JsonConvert.DeserializeObject<List<Category>>(response.Content.ReadAsStringAsync().Result);
                categories = CollectiveMethods.flattenCategoryList(categories);
            }
            return categories;
        }
        //Index page
        [HttpGet]
        public IActionResult Index()
        {
            return View(GetCategories()) ;
        }
        //Create page
        public IActionResult Create()
        {
            CategoryViewModel categoryViewModel = new CategoryViewModel();
            categoryViewModel.categories = GetCategories();
            return View(categoryViewModel);
        }
        //Update page
        public IActionResult Update(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var response = _httpClient.GetAsync(URL+"/"+id.ToString()).Result;
            if (response == null)
            {
                return NotFound();
            }
            NewCategoryDTO responseDTO = JsonConvert.DeserializeObject<NewCategoryDTO>(response.Content.ReadAsStringAsync().Result);
            CategoryViewModel categoryViewModel = new CategoryViewModel();
            categoryViewModel.newCategoryDTO = responseDTO;
            categoryViewModel.categories = GetCategories();
            return View(categoryViewModel);
        }
        //Delete page
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var response = _httpClient.GetAsync(URL + "/" + id.ToString()).Result;
            if (response == null)
            {
                return NotFound();
            }
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return NotFound();
            }
            NewCategoryDTO responseDTO = JsonConvert.DeserializeObject<NewCategoryDTO>(response.Content.ReadAsStringAsync().Result);
            return View(responseDTO);
        }
        //Functions
        //Post
        [HttpPost]
        public IActionResult Create(NewCategoryDTO newCategoryDTO)
        {
            if (ModelState.IsValid)
            {
               var response = _httpClient.PostAsJsonAsync(URL, newCategoryDTO).Result;
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return RedirectToAction("Index");
                }
                return View();
            }
            IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
            return View(newCategoryDTO);
        }
        //Put
        [HttpPost]
        public IActionResult Update(int? id, NewCategoryDTO newCategoryDTO)
        {
            if (ModelState.IsValid)
            {
                var response = _httpClient.PutAsJsonAsync(URL + "?id=" + id.ToString(), newCategoryDTO).Result;

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Error has occured");
                }
            }
            IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
            return View(new CategoryViewModel { newCategoryDTO = newCategoryDTO , categories = GetCategories()}); 
        }
        //Delete
        [HttpPost]
        public IActionResult DeletePost(int? id)
        {
            var response = _httpClient.DeleteAsync(URL + "?id=" + id.ToString()).Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("Index");
            }
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return View("Error has occured");
            }
            return View();
        }
    }
}