using EShop.Models.DTOS;
using EShop.Models;
using EShop.Models.StaticClasses;
using EShop.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace EShop.Controllers
{
    public class ProductController : Controller
    {
        private static HttpClient _httpClient;
        private string URL = "https://eshopgunapi.azurewebsites.net/api/Products/";

        static ProductController()
        {
            _httpClient = new HttpClient();
        }
        [HttpGet]
        public IActionResult Index(int id, int varId)
        {
            var response = _httpClient.GetAsync(URL + id + "/" + varId).Result;
            ProductVariantViewModel responseDTO = new ProductVariantViewModel();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {

                responseDTO.setAttributesForNewVariant(JsonConvert.DeserializeObject<ProductWithASingleVariant>(response.Content.ReadAsStringAsync().Result));
            }
            return View("Index",responseDTO);
        }

        public IActionResult addToCart(ProductVariantViewModel variant)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionKeyManager.SessionKeyForUsers)))
            {
                TempData["Error"] = "User must be logged in in order to add items to cart";
                return Index(variant.ProductId, variant.VariantId);
            }
            else
            {
                if (string.IsNullOrEmpty(Request.Cookies[SessionKeyManager.UserCookie]))
                {

                    List<OrderedVariant> package = new List<OrderedVariant>
                        {
                            new OrderedVariant
                            {
                                Id = variant.VariantId,
                                ProductId = variant.ProductId,
                                Quantity = variant.Quantity,
                            }
                        };
                    string cookieValue = JsonConvert.SerializeObject(package);
                    CreateCookie(cookieValue);
                }
                else
                {
                    List<OrderedVariant> package = JsonConvert.DeserializeObject<List<OrderedVariant>>(
                        Request.Cookies[SessionKeyManager.UserCookie]);
                    if (package
                        .FirstOrDefault(o => o.Id == variant.VariantId)
                        != null)
                    {
                        package
                        .FirstOrDefault(o => o.Id == variant.VariantId)
                        .Quantity += variant.Quantity;
                    }
                    else
                    {
                        package.Add(new OrderedVariant
                        {
                            Id = variant.VariantId,
                            ProductId = variant.ProductId,
                            Quantity = variant.Quantity,
                        });
                    }
                    string cookieValue = JsonConvert.SerializeObject(package);
                    CreateCookie(cookieValue);
                }
                var response = _httpClient.GetAsync(URL + variant.ProductId + "/" + variant.VariantId).Result;
                ProductVariantViewModel responseDTO = new ProductVariantViewModel();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {

                    responseDTO.setAttributesForNewVariant(JsonConvert.DeserializeObject<ProductWithASingleVariant>(response.Content.ReadAsStringAsync().Result));
                }
                TempData["Sucess"] = "Item added";
                return RedirectToAction("index", "Order");
            }
        }

        private void CreateCookie(string cookieValue)
        {
            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(5)
            };
            Response.Cookies.Append(SessionKeyManager.UserCookie, cookieValue, options);
        }
    }
}
//Order history