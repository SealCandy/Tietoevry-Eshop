using EShop.Models.DTOS;
using EShop.Models;
using EShop.Models.StaticClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Newtonsoft.Json;
using System.Xml.Linq;

namespace EShop.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private static HttpClient _httpClient;
        private string URL = "https://eshopgunapi.azurewebsites.net/api/Customers";
        private string URLOrder = "https://eshopgunapi.azurewebsites.net/api/Orders";

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }
        static UserController()
        {
            _httpClient = new HttpClient();
        }
        //Index page
        public IActionResult Index()
        {
            return View();
        }
        //Login page
        //Passwd pogfish420
        public IActionResult UserPage()
        {
            if (ModelState.IsValid)
            {
                var customerDTO = JsonConvert.DeserializeObject<CustomerDTO>(HttpContext.Session.GetString(SessionKeyManager.SessionKeyForUsers));
                return View(customerDTO);

            }
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Login(CustomerDTO customerDTO)
        {
            if (ModelState.IsValid)
            {
                var response = _httpClient.GetAsync(URL + "?LoginName=" + customerDTO.Name + "&Password=" + customerDTO.Surname).Result;
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    customerDTO = JsonConvert.DeserializeObject<CustomerDTO>(response.Content.ReadAsStringAsync().Result);
                    //Add the whole customer
                    HttpContext.Session.SetString(SessionKeyManager.SessionKeyForUsers, JsonConvert.SerializeObject(customerDTO));
                    SessionKeyManager.SetUserCookieName(customerDTO.Id);
                    TempData["Sucess"] = "User sucessfully logged in";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["Error"] = "Failed to login";
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
        public IActionResult OrderHistory()
        {
            var customerDTO = JsonConvert.DeserializeObject<CustomerDTO>(HttpContext.Session.GetString(SessionKeyManager.SessionKeyForUsers));
            var response = _httpClient.GetAsync(URLOrder + "/" + customerDTO.Id.ToString() ).Result;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseDTO = JsonConvert.DeserializeObject<List<OrderDTO>>(response.Content.ReadAsStringAsync().Result);
                return View(responseDTO);
            }
            else
            {
                TempData["Error"] = "Could not get history";
                return RedirectToAction("Index","Home");
            }
        }
        [HttpPost]
        public IActionResult Update(CustomerDTO customerDTO)
        {
            var customerOLD= JsonConvert.DeserializeObject<CustomerDTO>(HttpContext.Session.GetString(SessionKeyManager.SessionKeyForUsers));
            if (ModelState.IsValid)
            {
                var response = _httpClient.PutAsJsonAsync(URL + "?id=" + customerOLD.Id.ToString(), customerDTO).Result;
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    customerDTO.Id = customerOLD.Id;
                    HttpContext.Session.SetString(SessionKeyManager.SessionKeyForUsers, JsonConvert.SerializeObject(customerDTO));
                    TempData["Sucess"] = "User sucessfully updated";
                    return View("UserPage");
                }
                TempData["Error"] = "Failed to update user";
                return View("UserPage");
            }
            TempData["Error"] = "Failed to update user";
            return View("UserPage");
        }
        public IActionResult LogOff()
        {
            HttpContext.Session.Remove(SessionKeyManager.SessionKeyForUsers);
            return RedirectToAction("Index", "Home");
        }
    }
}