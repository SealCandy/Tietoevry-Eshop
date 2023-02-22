using EShop.Models.DTOS;
using EShop.Models;
using EShop.Models.StaticClasses;
using EShop.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Drawing.Printing;
using System.Security.Policy;

namespace EShop.Controllers
{
    public class OrderController : Controller
    {
        private static HttpClient _httpClient;
        private string URLProduct = "https://eshopgunapi.azurewebsites.net/Products/";
        private string URLUser = "https://eshopgunapi.azurewebsites.net/Customers";
        private string URLOrders = "https://eshopgunapi.azurewebsites.net/api/Orders";

        static OrderController()
        {
            _httpClient = new HttpClient();
        }

        public IActionResult Index()
        {
            List<OrderedVariant> package = CreateOrderVariant();
            if (package == null)
            {
                return View();
            }
            List<ProductVariantViewModel> responseDto = CreateListOfVariantViewModel(package);

            return View(responseDto);
        }

        //Method for updating an item from a cookie
        public IActionResult UpdateCookie(int id, int quantity)
        {
            updateCookie(id, quantity);
            return RedirectToAction("Index", "Order");
        }
        //Method for deleting items from a cookie
        public IActionResult Delete(int id)
        {
            List <OrderedVariant> package = CreateOrderVariant();
            package
            .Remove(package.Where(o => o.Id == id ).FirstOrDefault());
            CreateCookie(package);
            return RedirectToAction("Index","Order");
        }

        public IActionResult FinishOrder()
        {
            List<OrderedVariant> package = CreateOrderVariant();
            List<ProductVariantViewModel> responseDto = CreateListOfVariantViewModel(package);
            UserOrderView userOrder = new UserOrderView();
            userOrder.ProductVariantViewModels = responseDto;
            userOrder.CustomerDTO = JsonConvert.DeserializeObject<CustomerDTO>(
                HttpContext.Session.GetString(SessionKeyManager.SessionKeyForUsers));
            return View(userOrder);
        }

        public IActionResult CreateOrder(CustomerDTO customerDTO)
        {
            if (updateUser(customerDTO))
            {
                var currentCustomer = JsonConvert.DeserializeObject<CustomerDTO>(HttpContext.Session.GetString(SessionKeyManager.SessionKeyForUsers));
                List<OrderedVariant> package = CreateOrderVariant();
                List<ProductVariantViewModel> responseDto = CreateListOfVariantViewModel(package);
                List<OrderLineDTO> orderLines = new List<OrderLineDTO>();
                foreach (var item in responseDto)
                {
                    orderLines.Add(new OrderLineDTO
                    {
                        Price = item.Price,
                        ProductVariantId = item.VariantId,
                        Quantity = item.Quantity,
                    });
                }
                NewOrderDTO order = new NewOrderDTO();
                order.OrderLinesDTO = orderLines;
                if (postOrder(order, currentCustomer.Id))
                {
                    Response.Cookies.Append(SessionKeyManager.UserCookie, "");
                    TempData["Sucess"] = "Order sucessfully created";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["Error"] = "Failed to create an order";
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                TempData["Error"] = "Failed to set destination adress";
                return RedirectToAction("Index", "Home");
            }
        }
        //HttpMethods
        private bool postOrder(NewOrderDTO order, int customerId)
        {
            var response = _httpClient.PostAsJsonAsync(URLOrders + "?CurrentCustomerId=" + customerId.ToString(), order).Result;
            return (response.StatusCode == System.Net.HttpStatusCode.OK);

        }
        private bool updateUser(CustomerDTO customerDTO)
        {
            var customerOLD = JsonConvert.DeserializeObject<CustomerDTO>(HttpContext.Session.GetString(SessionKeyManager.SessionKeyForUsers));
            if (ModelState.IsValid)
            {
                var response = _httpClient.PutAsJsonAsync(URLUser + "?id=" + customerOLD.Id.ToString(), customerDTO).Result;
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    customerDTO.Id = customerOLD.Id;
                    HttpContext.Session.SetString(SessionKeyManager.SessionKeyForUsers, JsonConvert.SerializeObject(customerDTO));
                    return true;
                }
                return false;
            }
            return false;
        }
        //Iternal methods for cookies
        private List<ProductVariantViewModel> CreateListOfVariantViewModel(List <OrderedVariant> package)
        {
            List<ProductVariantViewModel> responseDto = new List<ProductVariantViewModel>();
            foreach (var item in package)
            {
                var response = _httpClient.GetAsync(URLProduct + item.ProductId + "/" + item.Id).Result;

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    ProductVariantViewModel variant = new ProductVariantViewModel();
                    variant.setAttributes(JsonConvert.DeserializeObject<ProductWithASingleVariant>(response.Content.ReadAsStringAsync().Result));
                    variant.Quantity = item.Quantity;
                    responseDto.Add(variant);
                }
            }
            return responseDto;
        }
        private List<OrderedVariant> CreateOrderVariant()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionKeyManager.SessionKeyForUsers)) || 
                string.IsNullOrEmpty(Request.Cookies[SessionKeyManager.UserCookie]))
            {
                return null;
            }
            else
            {
                List<OrderedVariant> package = JsonConvert.DeserializeObject<List<OrderedVariant>>(
                Request.Cookies[SessionKeyManager.UserCookie]);
                return package;
            }
        }

        private void updateCookie(int variantId, int quantity)
        {
            List<OrderedVariant> package = JsonConvert.DeserializeObject<List<OrderedVariant>>(
            Request.Cookies[SessionKeyManager.UserCookie]);
            package.Where(o => o.Id == variantId)
                .Select(o => { o.Quantity = quantity; return o; })
                .ToList();
            CreateCookie(package);
        }
        private void CreateCookie(List<OrderedVariant> package)
        {
            string cookieValue = JsonConvert.SerializeObject(package);
            Response.Cookies.Append(SessionKeyManager.UserCookie, cookieValue);
        }
    }
}