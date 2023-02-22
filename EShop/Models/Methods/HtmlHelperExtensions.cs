using EShop.Models.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Collections.Generic;

namespace EShop.Models.Methods
{
    public static class HtmlHelperExtensions
    {
        public static HtmlString CategoryTree(this IHtmlHelper html, List<Category> categories)
        {
            string htmlOutput = string.Empty;
            if (categories != null && categories.Count > 0)
            {
                htmlOutput += "<ul class=\"list-group\">";
                foreach (Category category in categories)
                {
                    htmlOutput += "<li class=\"list-group-item\" >";
                    htmlOutput += "<span>";
                    htmlOutput += "<a id=\"btnclick\"href=\"\"role=\"button\" class=\"btn shadow-none showTxt collapsed\" data-bs-toggle=\"collapse\" data-bs-target=#take" + category.Id + "  >";
                    htmlOutput += "</a>";
                    htmlOutput += "</span>";
                    htmlOutput += "<a href=/Category/Index/"+category.Id+">";
                    htmlOutput += category.Name;
                    htmlOutput += "</a>";
                    if (category.SubCategories != null && category.SubCategories.Count > 0)
                    {
                        htmlOutput += ChildTree(category.SubCategories,category.Id);
                    }
                    htmlOutput += "</li>";
                }
                htmlOutput += "</ul>";
            }
            return new HtmlString(htmlOutput);
        }

        private static string ChildTree(List<Category> categories, int parentId)
        {
            string childHtml = string.Empty;
            foreach (Category category in categories)
            {
                childHtml += "<ul class=\"list-group collapsing\" id=take" + parentId+">";
                childHtml += "<li class=\"list-group-item\">";
                childHtml += "<span>";
                childHtml += "<a id=\"btnclick\" value=\"+\"href=\"\"role=\"button\" class=\"btn shadow-none showTxt collapsed\" data-bs-toggle=\"collapse\" data-bs-target=#take" + category.Id + "  >";
                childHtml += "</a>";
                childHtml += "</span>";
                childHtml += "<a href=/Category/Index/"+category.Id+">";
                childHtml += category.Name;
                childHtml += "</a>";
                if (category.SubCategories != null && category.SubCategories.Count > 0)
                {
                    childHtml += ChildTree(category.SubCategories,category.Id);
                }
                childHtml += "</li>";
                childHtml += "</ul>";
            }
            return childHtml;
        }
    }
}
