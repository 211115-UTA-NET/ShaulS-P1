using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GroceryAppConsole
{
    public  class OrderObject
    {

        string storeLocation;
        private int orderID;
        DateTime ordertime;
        double total;
        
        static readonly int maxQuantity = 100;
        //private StoresObject store;
        public StoresObject Store { get; set; }

        //        private Customer customer;
      //  public Customer Customer { get; set; }// { get => customer; set => customer = value; }
        public OrderObject()
        {
            this.Store = new StoresObject();
        //   this.Customer = new Customer();
        }
        //List<OrderLines> ordersLines;//= new List<OrderLines>();
        //public List<OrderLines> OrdersLines
        //{
        //    get
        //    {
        //        if (ordersLines == null)
        //        {
        //            ordersLines = new List<OrderLines>();
        //        }
        //        return ordersLines;
        //    }

        //    set
        //    {
        //        ordersLines = value;
        //    }
        //}

        //        private static IRepository? _repository;
        //        [JsonIgnore] public static IRepository? Repository { get => _repository; set => _repository = value; }

        //public static int MaxQuantity => maxQuantity;
        //public int CustomerId { get => Customer.CustomerId; set => Customer.CustomerId = value; }
        //public string StoreLocation { get => storeLocation; set => storeLocation = value; }

        //       public DateTime Ordertime { get => ordertime; set => ordertime = value; }
        //      public int OrderID { get => orderID; set => orderID = value; }
        //        public double Total { get => total; set => total = value; }




        /// <summary>
        /// Search Order By Id returns Order Object
        /// </summary>
        //public static Order SearchOrderById(int OrderId)
        //{
        //    return _repository is null ? null : _repository.SearchOrderById(OrderId);
        //}
        /// <summary>
        /// Check Order Valid Quantity returns the error Message in strSummary
        /// </summary>
        //public bool CheckOrderValidQuantity(ref string strSummary)
        //{
        //    bool result = true;
        //    var summary = new StringBuilder();
        //    foreach (var record in this.OrdersLines)
        //    {

        //        if (record.Quantity > MaxQuantity)
        //        {
        //            result = false;
        //            summary.AppendLine($"Product\t{record.OrderProduct.ProductName}\t{record.Quantity}");
        //        }
        //    }
        //    if (result == false)
        //    {
        //        summary.Insert(0, $"Order Was Rejected exceed max quntity of {MaxQuantity} on Products:\n");
        //        strSummary = summary.ToString();
        //    }
        //    return result;
        //}
        /// <summary>
        /// Add New Order with Validity check
        /// </summary>
        //public async Task<bool> AddNewOrder(ref string strSummary)
        //{
        //    bool result = false;
        //    result = this.CheckOrderValidQuantity(ref strSummary);
        //    if (result == true)
        //        return _repository is null ? false : await _repository.AddNewOrderAsync(this);
        //    else
        //        return false;
        //}
        /// <summary>
        /// Display Details Order
        /// </summary>

        //public string DisplayDetailsOrder()
        //{
        //    var summary = new StringBuilder();
        //    summary.AppendLine($"Store Name:{this.Store.LocationName}");
        //    summary.AppendLine($"Order Date:{this.Ordertime}\t\tCustomer Name: {this.customer.FirstName} {this.customer.LastName}\t\t");
        //    summary.AppendLine($"Product\t\tQuantity\t\tPrice\tAmount");
        //    summary.AppendLine("---------------------------------------------------------------");
        //    foreach (var record in this.OrdersLines)
        //    {
        //        summary.AppendLine($"{record.OrderProduct.ProductName }\t\t{record.Quantity}\t\t\t${record.OrderPrice}\t${record.Quantity * record.OrderPrice }");
        //    }
        //    summary.AppendLine("---------------------------------------------------------------");
        //    summary.AppendLine($"Order Total\t${this.Total} ");
        //    return summary.ToString();

        //}

        //public OrderObject(Customer UpdateCustomer, Stores UpdateStore)
        //{
        //    if (UpdateCustomer is not null) customer = UpdateCustomer;
        //    if (UpdateStore is not null) store = UpdateStore;
        //    this.ordertime = DateTime.Now;
        //}
        //public OrderObject(Customer UpdateCustomer, Stores UpdateStore, DateTime ordertime)
        //{
        //    if (UpdateCustomer is not null) customer = UpdateCustomer;
        //    if (UpdateStore is not null) store = UpdateStore;
        //    this.ordertime = ordertime;
        //}

        //public OrderObject(Customer UpdateCustomer, Stores UpdateStore, DateTime ordertime, double Total)
        //{
        //    if (UpdateCustomer is not null) customer = UpdateCustomer;
        //    if (UpdateStore is not null) store = UpdateStore;
        //    this.Total = Total;
        //    this.ordertime = ordertime;
        //}
        //public OrderObject(Customer UpdateCustomer, Stores UpdateStore, DateTime ordertime, double Total, int orderID)
        //{
        //    if (UpdateCustomer is not null) customer = UpdateCustomer;
        //    if (UpdateStore is not null) store = UpdateStore;
        //    this.Total = Total;
        //    this.ordertime = ordertime;
        //    this.orderID = orderID;
        //}
    }


}
