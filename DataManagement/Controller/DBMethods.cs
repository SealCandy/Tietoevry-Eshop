using CodeFirst2._0.Models;
using CodeFirst2._0.Enum;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodManagement.Controller
{
    /*
     TODO
    1.DELETE tables DONE
    2.ANONYMIZE Customers DONE
    3.PRINT ALL Catalog DONE
    4.PRINT LOCATIONS DONE
    5.PRINT CANCELLED ORDER BY CUSTOMERS DONE
    6.VARIANTS BOUGHT WITH
     */
    public class DBMethods
    {
        private ShopDbContext DatabaseShop = new();
        // 1.Done
        public int ClearAllTables()
        {
            int AllItems = (from c in DatabaseShop.Categories select c).Count();
            AllItems += (from c in DatabaseShop.Products select c).Count();
            AllItems += (from c in DatabaseShop.ProductVariants select c).Count();
            AllItems += (from c in DatabaseShop.Customers select c).Count();
            AllItems += (from c in DatabaseShop.Orders select c).Count();
            AllItems += (from c in DatabaseShop.OrderLines select c).Count();
            DatabaseShop.Database.ExecuteSqlRaw("Delete from Categories");
            DatabaseShop.Database.ExecuteSqlRaw("Delete from Customers");
            DatabaseShop.Database.ExecuteSqlRaw("Delete from Orders");
            DatabaseShop.Database.ExecuteSqlRaw("Delete from OrderLines");
            DatabaseShop.Database.ExecuteSqlRaw("Delete from Products");
            DatabaseShop.Database.ExecuteSqlRaw("Delete from ProductVariants");
            DatabaseShop.Database.ExecuteSqlRaw("Delete from CategoryProduct");
            DatabaseShop.SaveChanges();

            return AllItems;
        }
        public int ImportData_Customers(string pathToCsv)
        {
            return 0;
        } //return amoount of imported lines
        public int ImportData_Categories(string pathToCsv)
        {
            return 0;
        } //return amoount of imported lines
        // ...

        //2.Done

        public void AnonymizeOldUsers(DateTime olderThan)
        {
            Random Random = new Random();
            const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var CustomersBeforeDate = (from c in DatabaseShop.Orders where c.DateTime < olderThan select c.Customer).ToList();
            foreach (var Customer in CustomersBeforeDate)
            {
                Customer.Name = new string(Enumerable.Repeat(Chars, 10).Select(s => s[Random.Next(s.Length)]).ToArray());
                Customer.Surname = new string(Enumerable.Repeat(Chars, 10).Select(s => s[Random.Next(s.Length)]).ToArray());
                DatabaseShop.Customers.Update(Customer);
                Console.WriteLine("Name: " + Customer.Name + " Surname: " + Customer.Surname);
                DatabaseShop.SaveChanges();
            }


        }
        //Fix it up later
        public void PrintCompleteCatalog()
        {
            string Cross = " ├─";
            string Corner = " └─";
            string Pipe = "──";
            int Depth = 0;
            Console.WriteLine("Root");
            List<Category> Categories = DatabaseShop.Categories.Include(c => c.Products).ToList();
            List<ProductVariant> ProductVariant = DatabaseShop.ProductVariants.ToList();
            //google lazy loading for DB and eager loading
            foreach (var item in Categories)
            {

                if (item.SubCategories != null && !item.Equals(Categories.Last()) && item.ParentId == null)
                {
                    Console.WriteLine(Corner + Pipe + " " + item.Name);
                    Depth++;

                    LoopCategory(item.SubCategories, Depth);
                }
                else if (item.Equals(Categories.Last()) && item.SubCategories != null && item.ParentId == null)
                {
                    Console.WriteLine(Corner + Pipe + " " + item.Name);
                    Depth++;

                    LoopCategory(item.SubCategories, Depth);
                }
                else
                {

                }
            }

        }
        private void LoopCategory(List<Category> Categories, int Depth)
        {
            string Pipes = "";
            string Cross = " ├─";
            string Corner = " └─";
            string Vertical = " │";
            string Space = "   ";
            string Pipe = "──";
            foreach (var category in Categories)
            {
                for (int i = 0; i < Depth; i++)
                {
                    if (Categories.Last().Equals(category))
                    {
                        if (i <= 0 || i == Depth - 1)
                        {
                            Pipes += Space + Space;
                        }
                        else
                        {
                            Pipes += Vertical + Space;
                        }
                    }
                    else
                    {
                        if (i <= 0)
                        {
                            Pipes += Space + Space;
                        }
                        else
                        {
                            Pipes += Vertical + Space;
                        }
                    }
                }
                if (category.SubCategories != null && category.SubCategories.Count > 0)
                {
                    if (category.Equals(Categories.Last()))
                    {
                        Pipes += Corner;
                    }
                    else
                    {
                        Pipes += Cross;
                    }
                    Console.WriteLine(Pipes + " " + category.Name);
                    LoopCategory(category.SubCategories, Depth + 1);
                    Pipes = "";
                }
                else
                {
                    if (category.Equals(Categories.Last()))
                    {
                        Pipes += Corner;
                    }
                    else
                    {
                        Pipes += Cross;
                    }
                    Console.WriteLine(Pipes + " " + category.Name);
                    if (category.Products != null && category.Products.Count >= 0 && category.SubCategories == null)
                    {
                        Depth++;
                        LoopProduct(category.Products, Depth);
                        Depth--;
                    }
                    Pipes = "";

                }
            }
        }
        private void LoopProduct(List<Product> Products, int Depth)
        {
            string Pipes = "";
            string Cross = " ├─";
            string Corner = " └─";
            string Vertical = " │";
            string Space = "   ";
            string Pipe = "──";
            foreach (var Product in Products)
            {
                for (int i = 0; i < Depth; i++)
                {
                    if (i <= 0)
                    {
                        Pipes += Space + Space;
                    }
                    else
                    {
                        Pipes += Vertical + Space;
                    }
                }
                if (Product.Variants != null && Product.Variants.Count > 0)
                {
                    if (Product.Equals(Products.Last()))
                    {
                        Pipes += Corner;
                    }
                    else
                    {
                        Pipes += Cross;
                    }
                    Console.WriteLine(Pipes + " " + Product.Name);
                    Pipes = "";
                    Depth++;
                    foreach (var item in Product.Variants)
                    {
                        for (int i = 0; i < Depth; i++)
                        {
                            if (Products.Last().Equals(Product))
                            {
                                if (i <= 0 || i == Depth - 1)
                                {
                                    Pipes += Space + Space;
                                }
                                else
                                {
                                    Pipes += Vertical + Space;
                                }
                            }
                            else
                            {
                                if (i <= 0)
                                {
                                    Pipes += Space + Space;
                                }
                                else
                                {
                                    Pipes += Vertical + Space;
                                }
                            }
                        }
                        Console.WriteLine(Pipes + Cross + item.Code);
                        Console.WriteLine(Pipes + Cross + item.ParamName1);
                        Console.WriteLine(Pipes + Cross + item.ParamName2);
                        Console.WriteLine(Pipes + Corner + item.ParamName3);
                    }
                    Depth--;
                    Pipes = "";
                }
                else
                {
                    if (Product.Equals(Products.Last()))
                    {
                        Depth--;
                        Pipes += Corner;
                    }
                    else
                    {
                        Pipes += Cross;
                    }
                    Console.WriteLine(Pipes + " " + Product.Name);
                    Pipes = "";

                }
            }
        }
        //Root
        //├── Category 1
        //|   ├── Category 1.1
        //|   ├── Category 1.2
        //|   |    ├── Category 1.2.1
        //|   |    |   ├── Product 1
        //|   |    |   └── Product 2
        //|   |    |       ├── ProductVariantCode 1
        //|   |    |       ├── ProductVariant 2
        //|   |    |       ├── ProductVariant 3
        //|   |    |       ├── ProductVariant 4
        //|   |    |       ├── ProductVariant 5
        //|   |    |       └── ProductVariant 6
        //|   |    └── Category 1.2.2
        //|   └── Category 1.3
        //└── Category 2
        //6.Needs testing
        public void GetProductVariantsBoughtWith(int productVariantId)
        {
            List<Order> Orders = DatabaseShop.Orders.Include(o => o.OrderLines).Where(od => od.OrderState == State.DELIVERED).ToList();
            List<ProductVariant> AllProductVariants = DatabaseShop.ProductVariants.ToList();
            List<ProductVariant> QuantitiesOfProduct = new List<ProductVariant>();
            bool containsVariant;
            foreach (var item in Orders)
            {
                if (item.OrderLines.Count() > 1)
                {//Rename vars for readability
                    foreach (var ol in item.OrderLines)
                    {
                        if (ol.ProductVariant.Id == productVariantId)
                        {
                            containsVariant = true;
                            foreach (var o in item.OrderLines)
                            {
                                if (o.ProductVariant.Id != productVariantId)
                                {
                                    QuantitiesOfProduct.Add(ol.ProductVariant);
                                }
                            }
                            continue;
                        }
                    }
                }
            }
            var results = QuantitiesOfProduct.GroupBy(x => new { x.ParamName1 }).Select(group => new { Name = group.Key, Count = group.Count() }).OrderBy(x => x.Count);
            int end = 0;
            foreach (var item in results)
            {
                if (end == 3)
                {
                    break;
                }
                else
                {
                    Console.WriteLine(item.Name);
                }
                end++;
            }
        }
        // for given product variant return 3 most requently bought product variants bought together (sort by frequency)
        //4.Done
        public void PrintCustomerLocations()
        {
            var customers = DatabaseShop.Customers.GroupBy(x => new { x.City, x.Zip }).OrderBy(x => x.Key.City).Select(x => new { x.Key.City, x.Key.Zip, Count = x.Count() }).ToList();
            List<string> AlreadyDid = new List<string>();
            foreach (var item in customers)
            {
                if (AlreadyDid.Contains(item.City))
                {
                    Console.WriteLine("  " + item.Zip + "  : " + item.Count);
                }
                else
                {
                    Console.WriteLine(item.City + "\n  " + item.Zip + ": " + item.Count);
                    AlreadyDid.Add(item.City);
                }

            }
        } // Group by City and ZIP, show number of customers
        // Ostrava
        //   70030: 20
        //   70020: 2
        // Brno
        //   60225: 30
        //   60531: 3
        //5.Done
        public int GetNumberOfCustomersWhoCancelledOrder()
        {
            return DatabaseShop.Orders.Include(c => c.Customer).Where(g => g.OrderState == State.CANCELLED).Select(c => c.Customer).Distinct().Count();
        }
        //Ideally don't go manually through all customers/orders, use clever query :)


        //BONUS 1 DONE

        public void GetMostPopularCategory()
        {
            var DeliveredOrders = DatabaseShop.Orders.Include(o => o.OrderLines).Where(g => g.OrderState == State.DELIVERED).ToList();
            List<ProductVariant> AllVariants = DatabaseShop.ProductVariants.Include(p => p.Product.Categories).ToList();
            Console.WriteLine(ComapareCategories(GetCategories(DeliveredOrders)).Name);


        }
        private List<Category> GetCategories(List<Order> DeliveredOrders)
        {
            List<Category> AllCategories = DatabaseShop.Categories.ToList();
            List<OrderLine> OrderLine = new List<OrderLine>();
            List<Category> Category = new List<Category>();
            foreach (var Order in DeliveredOrders)
            {
                foreach (var DeliveredOrderLines in Order.OrderLines)
                {
                    OrderLine.Add(DeliveredOrderLines);
                }
            }
            foreach (var item in OrderLine)
            {
                foreach (var ProductCategory in item.ProductVariant.Product.Categories)
                {
                    Category.Add(ProductCategory);
                }
            }
            return Category;
        }
        private Category ComapareCategories(List<Category> Categories)
        {
            int highestCategory = 0;
            int currentCategoryNumber = 0;
            Category ret = null;
            foreach (var item in Categories)
            {
                foreach (var Category in Categories)
                {
                    if (item == Category)
                    {
                        currentCategoryNumber++;
                    }
                }
                if (currentCategoryNumber > highestCategory)
                {
                    highestCategory = currentCategoryNumber;
                    ret = item;
                }
            }
            return ret;
        }
        // return category with most bought products
        // BONUS 2 Done
        public void PrintTopCustomers()
        {
            int Timer = 0;
            var Customers = DatabaseShop.Customers.Include(c => c.Orders).ToList();
            var AllOrderLines = DatabaseShop.OrderLines.ToList();
            List<OrderLine> CustomersLines = new List<OrderLine>();
            foreach (var Customer in Customers)
            {
                CustomersLines.Add(new OrderLine()
                {
                    Id = Customer.Id,

                    Price = getMaxPriceOrder(Customer.Orders)
                });
            }
            var results = CustomersLines.OrderByDescending(p => p.Price).ToList();
            for (int i = 0; i < 3; i++)
            {
                foreach (var Customer in Customers)
                {
                    if (Customer.Id == results[i].Id)
                    {
                        Console.WriteLine(Customer.Name + " " + Customer.Surname + " Price: " + results[i].Price);
                    }
                }
            }
        }
        private decimal getMaxPriceOrder(List<Order> Orders)
        {
            decimal ret = 0;
            foreach (var Order in Orders)
            {
                foreach (var OrderLines in Order.OrderLines)
                {
                    ret += OrderLines.Price;
                }
            }
            return ret;
        }
        // return 3 customers who spent most money. Print lines in this format: "Name Surname (price Kč)"
        //BONUS 3 DONE
        public void PrintOrdersByCustomerLocations()
        {
            var customers = DatabaseShop.Orders.Include(c => c.Customer).GroupBy(c => new { c.Customer.City, c.Customer.Zip }).Select(x => new { x.Key.City, x.Key.Zip, Count = x.Count() }).Distinct().ToList();
            foreach (var item in customers)
            {
                Console.WriteLine(item.City + "\n   " + item.Zip + ": " + item.Count);
            }
            //return null;
        }
        // retrun report in bellow format. Number of orders for each ZIP code in each city. Ideally don't go manually through all orders, use clever query :)
        // Ostrava
        //   70030: 20
        //   70020: 2
        // Brno
        //   60225: 30
        //   60531: 3
        // ...

        /*********************************************************************************************API METHODS********************************************************/
        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await DatabaseShop.Categories.ToListAsync();
        }
    }
}
