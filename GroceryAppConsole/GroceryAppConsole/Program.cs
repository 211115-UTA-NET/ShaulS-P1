using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Data.SqlClient;

namespace GroceryAppConsole
{
    public class Program
    {
        /// <summary>
        /// Console read input as a string with letters only
        /// </summary>
        public static String ConsoleReadNameLine(string Caption)
        {
            string? NewValue;
            do
            {
                Console.WriteLine(Caption);
                NewValue = Console.ReadLine();
                if (NewValue == null || NewValue.Trim()=="")
                    Console.WriteLine("Empty Value,Try again");
                if (!IsAllLetters(NewValue))
                {
                    Console.WriteLine("Invalid Value,Character must be Letter ,Try again");
                    NewValue = null;
                }
            }
            while (NewValue == null || NewValue.Trim() == "");
            return NewValue;
        }
        /// <summary>
        /// Console Read Line a string
        /// </summary>
        public static String ConsoleReadLine(string Caption)
        {
            string? NewValue;
            do
            {
                Console.WriteLine(Caption);
                NewValue = Console.ReadLine();
                if (NewValue == null || NewValue.Trim() == "")
                    Console.WriteLine("Empty Value,Try again");
            }
            while (NewValue == null || NewValue.Trim() == "");
            return NewValue;
        }
        /// <summary>
        /// Console read Line a whole number
        /// </summary>
        public static int ConsoleIntegerReadLine(string Caption)
        {
            string? NewValue;
            do
            {
                Console.WriteLine(Caption);
                NewValue = Console.ReadLine();
                if (NewValue == null || NewValue.Trim() == "") Console.WriteLine("Empty Value,Try again");
                if (IsInteger(NewValue) == 0)
                {
                    Console.WriteLine("Invaild Value,Try again");
                    NewValue = null;
                }
            }
            while (NewValue == null || NewValue.Trim() == "");
            return Int32.Parse(NewValue);
        }
        /// <summary>
        /// test if string is an integer
        /// </summary>
        public static int IsInteger(string s)
        {
            int i;
            bool result = int.TryParse(s, out i);
            if (result == true && i > 0)
                return i;
            else
                return 0;

        }
        // test string if it has only letters
        public static bool IsAllLetters(string s)
        {
            foreach (char c in s)
            {
                if (!Char.IsLetter(c) && c!=' ')
                    return false;
            }
            return true;
        }
        /// <summary>
        /// Console Read Order Line
        /// </summary>
        public async static Task InputOrderLine(Order UserOrder)
        {
            do
            {
                Product FindProduct = null;
                FindProduct=await SearchProduct();
                int Quantity = ConsoleIntegerReadLine("Enter Product Quantity(whole number):");
                if (Quantity > 0)
                {
                    UserOrder.OrdersLines.Add(new(FindProduct, Quantity));
                }
            }
            while (ConsoleReadLine("Add Product to The Order?(y for yes,any Value for no)") == "y");

        }
        /// <summary>
        /// Search Product
        /// </summary>
        public async static Task<Product> SearchProduct()
        {
            bool Result = false;
            Product FindProduct;
            do
            {
                FindProduct = new Product();
                FindProduct.ProductName = ConsoleReadLine("enter product Name:");
                FindProduct = await FindProduct.SearchProductByName();
                if (FindProduct.ProductId != 0)
                {
                    Console.WriteLine("product Exists");
                    Result = true;
                }
                else
                    Console.WriteLine("product Not Found");
            }
            while (Result == false);
            return FindProduct;
        }
        public static async Task<Customer> SearchCustomer(Customer FindCustomer)
        {
            bool Result = false;
            bool? ResultNull = false;
            //{
            FindCustomer = new Customer();
            FindCustomer.FirstName = ConsoleReadNameLine("enter Customer first Name:");
            FindCustomer.LastName = ConsoleReadNameLine("enter Customer Last Name:");
            FindCustomer.CustomerId  = await  FindCustomer.SearchCustomersByNameAsync();
            if (FindCustomer.CustomerId != 0)
            {
                Console.WriteLine("Customer Exists");
                Result = true;
            }
            else
                Console.WriteLine("Customer Not Found");
            return FindCustomer;
        }

        public async static Task AddNewOrderWithValidation(Order CurrOrder)
        {
            string summuryErr = "";
            ReturnStruct Ret;
            Ret = await CurrOrder.AddNewOrder(summuryErr);            
            if (Ret.ReturnValue  != 0)
            {
                CurrOrder.OrderID=Ret.ReturnValue;
                await Console.Out.WriteLineAsync("New Order Added " + CurrOrder.OrderID);
                
            }
            else
            {
                await Console.Out.WriteLineAsync(Ret.ErrMessage);
                //Console.Out.WriteLineAsync(Ret.ErrMessage);
            }
        }        
        public async static Task Main(string[] args)
        {
            try
            {

            //Uri server = new("https://localhost:7258");                                
                Uri server = new("https://groceryapishaul.azurewebsites.net");
                IGroceryAPI GroceryAPI = new GroceryAPI(server);
                Customer.Repository = GroceryAPI;
                Stores.Repository = GroceryAPI;
                Product.Repository = GroceryAPI;
                Order.Repository = GroceryAPI;


                Stores CenterGrocery = new("Center Grocery",2);                
                Product Milk = new("Milk");
                Milk=await Milk.SearchProductByName();
                Product Bread = new("Bread");
                Bread=await Bread.SearchProductByName();
                Product Candy = new("Candy");
                Candy=await Candy.SearchProductByName();

                Customer Customer1 = new Customer("Yarden", "Stavi");
                Customer1.CustomerId = await Customer1.SearchCustomersByNameAsync();
                if (Customer1.CustomerId == 0) await Customer1.AddNewCustomer();

                Order Order1 = new(Customer1, CenterGrocery, new DateTime(2021, 12, 10));
                Order1.OrdersLines = new List<OrderLines> { new(Milk, 3), new(Bread, 2) };
                await AddNewOrderWithValidation(Order1);

                //Customer NewCustomer2 = new Customer();
                //NewCustomer2.FirstName = "shaul";
                //NewCustomer2.LastName = "stavi";
                //NewCustomer2.CustomerId=await NewCustomer2.SearchCustomersByNameAsync();
                //Stores CenterGrocery = new();
                //CenterGrocery.LocationName = "test";
                //Order NewOrder = new Order(NewCustomer2, CenterGrocery);
                //NewOrder.Store = CenterGrocery;
                // NewOrder.Customer = NewCustomer2;
                //  await GroceryAPI.SubmitOrderAsync(NewOrder);

                /*                
                                string connectionString = File.ReadAllText("C:/Users/shaul/Revature/db.txt");
                                IRepository repository = new SqlRepository(connectionString);
                                Customer.Repository = repository;
                                Product.Repository = repository;
                                Stores.Repository = repository;
                                Order.Repository = repository;
                                Stores CenterGrocery = new("Center Grocery");
                                CenterGrocery.SearchStoreByName();

                                Product Milk = new("Milk");
                                Milk.SearchProductByName();
                                Product Bread = new("Bread");
                                Bread.SearchProductByName();
                                Product Candy = new("Candy");
                                Candy.SearchProductByName();

                                Customer Customer1 = new Customer("Yarden", "Stavi");
                                if (Customer1.SearchCustomersByName() == false) Customer1.AddNewCustomer();

                                Order Order1 = new(Customer1, CenterGrocery, new DateTime(2021, 12, 10));
                                Order1.OrdersLines = new List<OrderLines> { new(Milk, 3), new(Bread, 2) };
                                AddNewOrderWithValidation(Order1);

                                Order Order2 = new(Customer1, CenterGrocery, new DateTime(2021, 12, 11));
                                Order2.OrdersLines = new List<OrderLines> { new(Candy, 3), new(Milk, 2) };
                                AddNewOrderWithValidation(Order2);


                                Customer Customer2 = new Customer("Orit", "Stavi Rif");
                                if (Customer2.SearchCustomersByName() == false) Customer2.AddNewCustomer();

                                Order Order3 = new(Customer2, CenterGrocery, new DateTime(2021, 12, 11));
                                Order3.OrdersLines = new List<OrderLines> { new(Bread, 3), new(Milk, 2), new(Milk, 1) };
                                AddNewOrderWithValidation(Order3);
                */

                Customer FindCustomer;
                string? InputString = "";
                do
                {
                    Console.WriteLine(
@"
        store Menu
        ----------
1. place orders to store locations for customers
2. add a new customer
3. search customers by name
4. display details of an order
5. display all order history of a store location
6. display all order history of a customer
7. Exit");
                    InputString = Console.ReadLine();
                    if (InputString != "7")
                    {
                        switch (InputString)
                        {
                            case "1":
                                Console.WriteLine(await Stores.DisplayStoreList());
                                int StroeId = ConsoleIntegerReadLine("Enter Store ID: ");
                                Stores UserStore = new Stores(StroeId);
                                Console.WriteLine("Search Customer:");
                                FindCustomer = new Customer();
                                FindCustomer = await SearchCustomer(FindCustomer);
                                if (FindCustomer.CustomerId!=0)
                                {
                                 //   Console.WriteLine(FindCustomer.CustomerId);
                                    Order OrderUser = new Order(FindCustomer, UserStore);
                                    await InputOrderLine(OrderUser);
                                    await AddNewOrderWithValidation(OrderUser);
                                }
                                break;
                            case "2":
                                Customer NewCustomer = new Customer();
                                NewCustomer.FirstName = ConsoleReadNameLine("enter Customer first Name:");
                                NewCustomer.LastName = ConsoleReadNameLine("enter Customer Last Name:");
                                if ( await NewCustomer.SearchCustomersByNameAsync() == 0)
                                    await NewCustomer.AddNewCustomer();
                                else
                                    Console.WriteLine("Customer Already Exists");
                                break;

                            case "3":
                                Console.WriteLine("Search Customer:");
                                FindCustomer = new Customer();
                                await SearchCustomer(FindCustomer);
                                break;
                            case "4":
                                int OrderId = ConsoleIntegerReadLine("Enter Order Number:");
                                Order FindOrder = await Order.SearchOrderById(OrderId);
                                if (FindOrder is null)
                                    Console.Write("Order Not Found");
                                else
                                    Console.Write(FindOrder.DisplayDetailsOrder());
                                break;
                            case "5":
                                Console.WriteLine(await Stores.DisplayStoreList());
                                int StroeId2 = ConsoleIntegerReadLine("Enter Store ID: ");
                                Stores UserStore2 = new Stores(StroeId2);
                                Console.Write(await UserStore2.orderHistoryByStore());
                                break;
                            case "6":
                                Console.WriteLine("Search Customer:");
                                FindCustomer = new Customer();
                                FindCustomer=await SearchCustomer(FindCustomer);
                                if (FindCustomer.CustomerId!=0)
                                Console.Write(await FindCustomer.orderHistoryByCustomer());
                                break;
                            default:
                                Console.WriteLine("Not a Vaild Line Menu Number");
                                break;
                        }
                    }

                }
                while (InputString != "7");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
    }
}