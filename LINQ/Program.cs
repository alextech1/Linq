using LINQ.Helpers;
using LINQ.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LINQ
{
    class Program
    {
        static void Main()
        {
            //PrintAllProducts();
            //PrintAllCustomers();
            Exercise13();

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        #region "Sample Code"
        /// <summary>
        /// Sample, load and print all the product objects
        /// </summary>
        static void PrintAllProducts()
        {
            List<Product> products = DataLoader.LoadProducts();
            PrintProductInformation(products);
        }

        /// <summary>
        /// This will print a nicely formatted list of products
        /// </summary>
        /// <param name="products">The collection of products to print</param>
        static void PrintProductInformation(IEnumerable<Product> products)
        {
            string line = "{0,-5} {1,-35} {2,-15} {3,6:c} {4,6}";
            Console.WriteLine(line, "ID", "Product Name", "Category", "Unit", "Stock");
            Console.WriteLine("==============================================================================");

            foreach (var product in products)
            {
                Console.WriteLine(line, product.ProductID, product.ProductName, product.Category,
                    product.UnitPrice, product.UnitsInStock);
            }

        }

        /// <summary>
        /// Sample, load and print all the customer objects and their orders
        /// </summary>
        static void PrintAllCustomers()
        {
            var customers = DataLoader.LoadCustomers();
            PrintCustomerInformation(customers);
        }

        /// <summary>
        /// This will print a nicely formated list of customers
        /// </summary>
        /// <param name="customers">The collection of customer objects to print</param>
        static void PrintCustomerInformation(IEnumerable<Customer> customers)
        {
            foreach (var customer in customers)
            {
                Console.WriteLine("==============================================================================");
                Console.WriteLine(customer.CompanyName);
                Console.WriteLine(customer.Address);
                Console.WriteLine("{0}, {1} {2} {3}", customer.City, customer.Region, customer.PostalCode, customer.Country);
                Console.WriteLine("p:{0} f:{1}", customer.Phone, customer.Fax);
                Console.WriteLine();
                Console.WriteLine("\tOrders");
                foreach (var order in customer.Orders)
                {
                    Console.WriteLine("\t{0} {1:MM-dd-yyyy} {2,10:c}", order.OrderID, order.OrderDate, order.Total);
                }
                Console.WriteLine("==============================================================================");
                Console.WriteLine();
            }
        }
        #endregion

        /// <summary>
        /// Print all products that are out of stock.
        /// </summary>
        static void Exercise1()
        {
            List<Product> products = DataLoader.LoadProducts();
            //var outOfStock = products.Where(x => x.UnitsInStock == 0).ToList();

            List<Product> outOfStockProducts = (from stock in products
                                                where stock.UnitsInStock == 0
                                                select stock).ToList();

            string line = "{0,-5} {1,-35} {2,-15} {3,6:c} {4,6}";
            Console.WriteLine(line, "ID", "Product Name", "Category", "Unit", "Stock");
            Console.WriteLine("==============================================================================");

            foreach (var product in outOfStockProducts)
            {
                Console.WriteLine(line, product.ProductID, product.ProductName, product.Category,
                    product.UnitPrice, product.UnitsInStock);
            }
        }

        /// <summary>
        /// Print all products that are in stock and cost more than 3.00 per unit.
        /// </summary>
        static void Exercise2()
        {
            List<Product> products = DataLoader.LoadProducts();
            //var stock = products.Where(x => x.UnitsInStock >= 1 && x.UnitPrice >= 3).ToList();

            List<Product> stockAndCostMoreThan3 = (from stock in products
                                                   where stock.UnitsInStock >= 1
                                                   && stock.UnitPrice >= 3
                                                   select stock).ToList();

            string line = "{0,-5} {1,-35} {2,-15} {3,6:c} {4,6}";
            Console.WriteLine(line, "ID", "Product Name", "Category", "Unit", "Stock");
            Console.WriteLine("==============================================================================");

            foreach (var product in stockAndCostMoreThan3)
            {
                Console.WriteLine(line, product.ProductID, product.ProductName, product.Category,
                    product.UnitPrice, product.UnitsInStock);
            }
        }

        /// <summary>
        /// Print all customer and their order information for the Washington (WA) region.
        /// </summary>
        static void Exercise3()
        {
            List<Customer> customers = DataLoader.LoadCustomers();
            //var waRegion = customers.Where(x => x.Region == "WA").ToList();

            List<Customer> waRegion = (from x in customers
                                       where x.Region == "WA"
                                       select x).ToList();

            foreach (var customer in waRegion)
            {
                Console.WriteLine("==============================================================================");
                Console.WriteLine(customer.CompanyName);
                Console.WriteLine(customer.Address);
                Console.WriteLine("{0}, {1} {2} {3}", customer.City, customer.Region, customer.PostalCode, customer.Country);
                Console.WriteLine("p:{0} f:{1}", customer.Phone, customer.Fax);
                Console.WriteLine();
                Console.WriteLine("\tOrders");
                foreach (var order in customer.Orders)
                {
                    Console.WriteLine("\t{0} {1:MM-dd-yyyy} {2,10:c}", order.OrderID, order.OrderDate, order.Total);
                }
                Console.WriteLine("==============================================================================");
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Create and print an anonymous type with just the ProductName
        /// </summary>
        static void Exercise4()
        {
            List<Product> products = DataLoader.LoadProducts();

            var anonProductName = products.Select(x => new { ProdName = x.ProductName }).ToList();

            string line = "{0,-15}";
            Console.WriteLine(line, "Product Name");
            Console.WriteLine("==============================================================================");

            foreach (var product in anonProductName)
            {
                Console.WriteLine(line, product.ProdName);
            }
        }

        /// <summary>
        /// Create and print an anonymous type of all product information but increase the unit price by 25%
        /// </summary>
        static void Exercise5()
        {
            List<Product> products = DataLoader.LoadProducts();

            var anonProductName = products.Select(x => new { ProdId = x.ProductID, ProdName = x.ProductName,
                Cat = x.Category, UnitPriceInc = x.UnitPrice * 1.25m, Stock = x.UnitsInStock }).ToList();

            string line = "{0,-5} {1,-35} {2,-15} {3,6:c} {4,6}";
            Console.WriteLine(line, "ID", "Product Name", "Category", "Price", "Stock");
            Console.WriteLine("==============================================================================");

            foreach (var product in anonProductName)
            {
                Console.WriteLine(line, product.ProdId, product.ProdName, product.Cat, product.UnitPriceInc,
                    product.Stock);
            }
        }

        /// <summary>
        /// Create and print an anonymous type of only ProductName and Category with all the letters in upper case
        /// </summary>
        static void Exercise6()
        {
            List<Product> products = DataLoader.LoadProducts();

            var anonProductName = products.Select(x => new { Id = x.ProductID, ProdName = x.ProductName.ToUpper(),
                Cat = x.Category.ToUpper() }).ToList();

            string line = "{0,-5} {1,-35} {2,-15}";
            Console.WriteLine(line, "ID", "Product Name", "Category");
            Console.WriteLine("==============================================================================");

            foreach (var product in anonProductName)
            {
                Console.WriteLine(line, product.Id, product.ProdName, product.Cat);
            }
        }

        /// <summary>
        /// Create and print an anonymous type of all Product information with an extra bool property ReOrder which should 
        /// be set to true if the Units in Stock is less than 3
        /// 
        /// Hint: use a ternary expression
        /// </summary>
        static void Exercise7()
        {
            List<Product> products = DataLoader.LoadProducts();

            var anonProductName = products.Select(x => new {
                Id = x.ProductID,
                ProdName = x.ProductName,
                Cat = x.Category,
                Price = x.UnitPrice,
                Stock = x.UnitsInStock,
                ReOrder = x.UnitsInStock < 3
            }).ToList();

            string line = "{0,-5} {1,-35} {2,-15} {3,6:c} {4,6} {5,7}";
            Console.WriteLine(line, "ID", "Product Name", "Category", "Unit", "Stock", "Reorder");
            Console.WriteLine("==============================================================================");

            foreach (var product in anonProductName)
            {
                Console.WriteLine(line, product.Id, product.ProdName, product.Cat,
                    product.Price, product.Stock, product.ReOrder);
            }
        }

        /// <summary>
        /// Create and print an anonymous type of all Product information with an extra decimal called 
        /// StockValue which should be the product of unit price and units in stock
        /// </summary>
        static void Exercise8()
        {
            List<Product> products = DataLoader.LoadProducts();

            var anonProductName = products.Select(x => new {
                Id = x.ProductID,
                ProdName = x.ProductName,
                Cat = x.Category,
                Price = x.UnitPrice,
                Stock = x.UnitsInStock,
                StockValue = x.UnitPrice * x.UnitsInStock
            }).ToList();

            string line = "{0,-5} {1,-35} {2,-15} {3,6:c} {4,6} {5,7}";
            Console.WriteLine(line, "ID", "Product Name", "Category", "Unit", "Stock", "StockValue");
            Console.WriteLine("======================================================================================");

            foreach (var product in anonProductName)
            {
                Console.WriteLine(line, product.Id, product.ProdName, product.Cat,
                    product.Price, product.Stock, product.StockValue);
            }
        }

        /// <summary>
        /// Print only the even numbers in NumbersA
        /// </summary>
        static void Exercise9()
        {
            int[] numbersA = DataLoader.NumbersA;

            //var linqEvenNums = numbersA.Where(num => num % 2 == 0).ToList();

            var lambdaEvenNums = (from num in numbersA
                                  where num % 2 == 0
                                  select num).ToList();
            foreach (var num in lambdaEvenNums)
            {
                Console.WriteLine(num);
            }


        }

        /// <summary>
        /// Print only customers that have an order whos total is less than $500
        /// </summary>
        static void Exercise10()
        {
            List<Customer> customers = DataLoader.LoadCustomers();

            var customerOrder = (from customer in customers
                                 from order in customer.Orders
                                 where order.Total < 500
                                 select customer).ToList();

            foreach (var customer in customerOrder)
            {
                Console.WriteLine("==============================================================================");
                Console.WriteLine(customer.CompanyName);
                Console.WriteLine(customer.Address);
                Console.WriteLine("{0}, {1} {2} {3}", customer.City, customer.Region, customer.PostalCode, customer.Country);
                Console.WriteLine("p:{0} f:{1}", customer.Phone, customer.Fax);
                Console.WriteLine();
                Console.WriteLine("==============================================================================");
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Print only the first 3 odd numbers from NumbersC
        /// </summary>
        static void Exercise11() //check again
        {
            int[] numbersC = DataLoader.NumbersC;

            //var linqOddNums = numbersC.Where(num => num % 2 != 0).ToList().Take(3);

            var lambdaOddNums = (from num in numbersC
                                 where num % 2 != 0
                                 select num).ToList().Take(3);

            foreach (var num in lambdaOddNums)
            {
                Console.WriteLine(num);
            }
        }

        /// <summary>
        /// Print the numbers from NumbersB except the first 3
        /// </summary>
        static void Exercise12()
        {
            var numbersB = DataLoader.NumbersB.Skip(3);
            foreach (var num in numbersB)
                Console.WriteLine(num);
        }

        /// <summary>
        /// Print the Company Name and most recent order for each customer in Washington
        /// </summary>
        static void Exercise13()
        {
            List<Customer> customers = DataLoader.LoadCustomers();

            var ordersGroup = (from customer in customers
                              where customer.Region == "WA"
                              select new
                              {
                                  Comp = customer.CompanyName,
                                  CustOrder = customer.Orders.OrderByDescending(x => x.OrderDate).Select(x => x.OrderDate).FirstOrDefault()
                              });

            //string line = "{0,-40} {1,-35}";
            //Console.WriteLine("==============================================================================");
            //Console.WriteLine(line, "Date", "Id");

            foreach (var cust in ordersGroup)
            {
                Console.WriteLine(cust.Comp);
                Console.WriteLine(cust.CustOrder);
            }
        }
        /// <summary>
        /// Print all the numbers in NumbersC until a number is >= 6
        /// </summary>
        static void Exercise14()
        {
            var numbersC = DataLoader.NumbersC;

            var numGreater = numbersC.Where(x => x >= 6);
            foreach (var num in numGreater)
                Console.WriteLine(num);
        }

        /// <summary>
        /// Print all the numbers in NumbersC that come after the first number divisible by 3
        /// </summary>
        static void Exercise15()
        {
            var numbersC = DataLoader.NumbersC;

            var divisable = (from num in numbersC
                             where num % 3 == 0 && num != 0
                             select num);

            foreach (var num in divisable)
            {
                Console.WriteLine(num);
            }
        }

        /// <summary>
        /// Print the products alphabetically by name
        /// </summary>
        static void Exercise16()
        {
            List<Product> products = DataLoader.LoadProducts();

            var productsSort = (from product in products
                                orderby product.ProductName
                                select product).ToList();

            string line = "{0,-5} {1,-35} {2,-15} {3,6:c} {4,6} {5,7}";
            Console.WriteLine(line, "ID", "Product Name", "Category", "Unit", "Stock", "StockValue");
            Console.WriteLine("======================================================================================");

            foreach (var product in productsSort)
            {
                Console.WriteLine(line, product.ProductID, product.ProductName, product.Category,
                    product.UnitPrice, product.UnitsInStock, product.UnitPrice);
            }
        }

        /// <summary>
        /// Print the products in descending order by units in stock
        /// </summary>
        static void Exercise17()
        {
            List<Product> products = DataLoader.LoadProducts();

            var productsSort = (from product in products
                                orderby product.UnitsInStock descending
                                select product).ToList();

            string line = "{0,-5} {1,-35} {2,-15} {3,6:c} {4,6} {5,7}";
            Console.WriteLine(line, "ID", "Product Name", "Category", "Unit", "Stock", "StockValue");
            Console.WriteLine("======================================================================================");

            foreach (var product in productsSort)
            {
                Console.WriteLine(line, product.ProductID, product.ProductName, product.Category,
                    product.UnitPrice, product.UnitsInStock, product.UnitPrice);
            }
        }

        /// <summary>
        /// Print the list of products ordered first by category, then by unit price, from highest to lowest.
        /// </summary>
        static void Exercise18()
        {
            List<Product> products = DataLoader.LoadProducts();

            var productsSort = (from product in products
                                orderby product.Category, product.UnitPrice descending
                                select product).ToList();

            string line = "{0,-5} {1,-35} {2,-15} {3,6:c} {4,6} {5,7}";
            Console.WriteLine(line, "ID", "Product Name", "Category", "Unit", "Stock", "StockValue");
            Console.WriteLine("======================================================================================");

            foreach (var product in productsSort)
            {
                Console.WriteLine(line, product.ProductID, product.ProductName, product.Category,
                    product.UnitPrice, product.UnitsInStock, product.UnitPrice);
            }
        }

        /// <summary>
        /// Print NumbersB in reverse order
        /// </summary>
        static void Exercise19()
        {
            var numbersB = DataLoader.NumbersB;

            List<int> numbersBreverse = (from numbers in numbersB
                                         orderby numbers descending
                                         select numbers).ToList();

            foreach (var num in numbersBreverse)
            {
                Console.WriteLine(num);
            }
        }

        /// <summary>
        /// Group products by category, then print each category name and its products
        /// ex:
        /// 
        /// Beverages
        /// Tea
        /// Coffee
        /// 
        /// Sandwiches
        /// Turkey
        /// Ham
        /// </summary>
        static void Exercise20()
        {
            List<Product> products = DataLoader.LoadProducts();

            var productsSort = (from product in products
                                group product by product.Category into newGroup
                                select newGroup).ToList();

            string line = "{0,-35}";
            Console.WriteLine(line, "Category");
            Console.WriteLine("======================================================================================");

            foreach (var productGroup in productsSort)
            {
                Console.WriteLine();
                Console.WriteLine(productGroup.Key);

                foreach (var product in productGroup)
                {
                    Console.WriteLine(line, product.ProductName);
                }
            }
        }

        /// <summary>
        /// Print all Customers with their orders by Year then Month
        /// ex:
        /// 
        /// Joe's Diner
        /// 2015
        ///     1 -  $500.00
        ///     3 -  $750.00
        /// 2016
        ///     2 - $1000.00
        /// </summary>
        static void Exercise21()
        {
            List<Customer> customers = DataLoader.LoadCustomers();

            string line = "{0,-35}";
            Console.WriteLine(line, "Category");
            Console.WriteLine("======================================================================================");

            foreach (var customer in customers)
            {
                Console.WriteLine();
                Console.WriteLine(customer.CompanyName);
                var ordersListGroup = (from x in customer.Orders group x by x.OrderDate);

                foreach (var ordersList in ordersListGroup)
                {
                    Console.WriteLine(ordersList.Key);
                    Console.WriteLine(line, (from y in ordersList select y.Total).Sum());
                }
            }
        }

        /// <summary>
        /// Print the unique list of product categories
        /// </summary>
        static void Exercise22()
        {
            List<Product> products = DataLoader.LoadProducts();

            var productsSort = (from product in products
                                group product by product.Category into newGroup
                                select newGroup).ToList();

            string line = "{0,-35}";
            Console.WriteLine(line, "Category");
            Console.WriteLine("======================================================================================");

            foreach (var productGroup in productsSort)
            {
                Console.WriteLine();
                Console.WriteLine(productGroup.Key);
            }
        }

        /// <summary>
        /// Write code to check to see if Product 789 exists
        /// </summary>
        static void Exercise23()
        {
            var products = DataLoader.LoadProducts();

            string line = "{0,-35}";
            Console.WriteLine(line, "Category");
            Console.WriteLine("======================================================================================");

            products.ForEach(x =>
            {
                if (x.ProductID == 789)
                    Console.WriteLine(x.ProductID);
                else
                    Console.WriteLine("Product 789 Not Found");
                return;
            });

        }

        /// <summary>
        /// Print a list of categories that have at least one product out of stock
        /// </summary>
        static void Exercise24()
        {
            var products = DataLoader.LoadProducts();

            var outOfStock = products.Where(x => x.UnitsInStock == 0).ToList().Select(x => x.Category);

            string line = "{0,-35}";
            Console.WriteLine(line, "Category");
            Console.WriteLine("======================================================================================");

            foreach (var stock in outOfStock)
                Console.WriteLine($"Out of stock - {stock}");
        }

        /// <summary>
        /// Print a list of categories that have no products out of stock
        /// </summary>
        static void Exercise25()
        {
            List<Product> products = DataLoader.LoadProducts();

            var productsSort = (from product in products
                                where product.UnitsInStock != 0
                                group product by product.Category into newGroup
                                select newGroup).ToList();

            string line = "{0,-35}";
            Console.WriteLine(line, "Category");
            Console.WriteLine("======================================================================================");

            foreach (var product in productsSort)
            {
                Console.WriteLine(product.Key);
            }
        }

        /// <summary>
        /// Count the number of odd numbers in NumbersA
        /// </summary>
        static void Exercise26()
        {
            var numbersA = DataLoader.NumbersA;

            var countOdd = numbersA.Select(x => x - '0').Count(x => x % 2 != 0);
            Console.WriteLine(countOdd);
        }

        /// <summary>
        /// Create and print an anonymous type containing CustomerId and the count of their orders
        /// </summary>
        static void Exercise27()
        {
            var customers = DataLoader.LoadCustomers();

            var ordersGroup = from customer in customers
                              group customer by new { customer.CustomerID, customer.Orders } into orders
                              select new
                              {
                                  CustID = orders.Key.CustomerID,
                                  Quantity = orders.Key.Orders.GroupBy(x => x.OrderID).Count()
                              };


            string line = "{0,-15} {1, -15}";
            Console.WriteLine(line, "Customer", "Count of orders");
            Console.WriteLine("======================================================================================");

            foreach (var order in ordersGroup)
            {
                Console.WriteLine($"{order.CustID, -15} {order.Quantity}");
            }
        }

        /// <summary>
        /// Print a distinct list of product categories and the count of the products they contain
        /// </summary>
        static void Exercise28()
        {
            List<Product> products = DataLoader.LoadProducts();

            var ordersGroup = from x in products
                              group x by new { x.Category } into newGroup
                              select new
                              {
                                  Cat = newGroup.Key.Category,
                                  Count = newGroup.Key.Category.Count()
                              };


            string line = "{0,-35}";
            Console.WriteLine(line, "Category");
            Console.WriteLine("======================================================================================");

            foreach (var product in ordersGroup)
            {
                Console.WriteLine(product.Cat);
                Console.WriteLine(product.Count);
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Print a distinct list of product categories and the total units in stock
        /// </summary>
        static void Exercise29()
        {
            List<Product> products = DataLoader.LoadProducts();

            var productsGroup = from x in products
                              group x by new { x.Category } into newGroup
                              select new
                              {
                                  Cat = newGroup.Key.Category,
                                  InStock = (from y in newGroup group y by y.UnitsInStock into totalStock select totalStock.Key).Sum()
                              };

            string line = "{0,-35}";
            Console.WriteLine(line, "Category");
            Console.WriteLine("======================================================================================");

            foreach (var product in productsGroup)
            {
                Console.WriteLine(product.Cat);
                Console.WriteLine($"Units in stock: {product.InStock}");
                
            }
        }

        /// <summary>
        /// Print a distinct list of product categories and the lowest priced product in that category
        /// </summary>
        static void Exercise30()
        {
            List<Product> products = DataLoader.LoadProducts();
            //LowestPrice = (from y in newGroup orderby y.UnitPrice select y.UnitPrice).ToList()

            var productsGroup = from x in products                                
                                group x by new { x.Category } into newGroup
                                select new
                                {
                                    Cat = newGroup.Key.Category,
                                    LowestPrice = newGroup.Min(x => x.UnitPrice)
                                };

            string line = "{0,-35}";
            Console.WriteLine(line, "Category");
            Console.WriteLine("======================================================================================");

            foreach (var product in productsGroup)
            {
                Console.WriteLine(product.Cat);
                Console.WriteLine(product.LowestPrice);
                
            }
        }

        /// <summary>
        /// Print the top 3 categories by the average unit price of their products
        /// </summary>
        static void Exercise31()
        {
            var products = DataLoader.LoadProducts();

            var category = (from x in products
                            group x by x.Category into newGroup
                            orderby products.Average(p => p.UnitPrice)
                            select newGroup.Key).Take(3);

            string line = "{0,-35}";
            Console.WriteLine(line, "Category", "Avg Price");
            Console.WriteLine("======================================================================================");

            foreach (var product in category)
            {
                Console.WriteLine(line, product);
            }
        }
    }
}

/*
 1. Exercises 4,5,6 - I am not sure who "Chang" but you are instead of grabbing the value from the list setting the value equal to that name. Also, while you are doing the requirements 
 (capitalization, adding 25%) in the display, you want to handle that calculation/update in the anon type declaration.
 2. Double check all of the prompts that ask for an anonymous type to be sure you are creating an object with the properties and not just manipulating the values in the display.
 3. #9,#11,#12,#14,#15,#23,#24,#26 - you are not using LINQ here. LINQ is used to replace looping in most instances.
 4. #13 - You are not doing anything to get the most recent order as prompted.
 5. #19 - Not using LINQ here - but this one has a LINQ keyword you can specifically use to accomplish your goal. Go check them out and see if you can find one that may help here.
 6. #25 - This is incorrect, you are trying to compare a name to "0" - 
 this has nothing to do with quantity of products in stock.
 7. #27,#28, #29, #30 use LINQ against the main list of products and pass that into the display, not use LINQ within the loop to display results.
     */
