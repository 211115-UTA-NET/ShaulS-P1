using System;
using System.Text;

namespace GroceryAPI
{

    /// <summary>
    /// Customer Record Update and insert data from Database
    /// </summary>
    public class Customer
    {

        private int customerId;

        private string firstName="";

        private string lastName="";

        public int CustomerId { get => customerId; set => customerId = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        private static IRepository? _repository;
        public static IRepository? Repository { get => _repository; set => _repository = value; }

        public Customer()
        { }
        public Customer(string FirstName, string LastName)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
        }
        /// <summary>
        /// Add New Customer Record To The Database 
        /// </summary>
        public void AddNewCustomer()
        {
            if (_repository is not null) _repository.AddNewCustomerAsync(this);
        }
//        public async Task<bool> SearchCustomersByName()
  //      {
    //        return _repository is null ? false : await _repository.SearchCustomersByNameAsync(this);
      //  }
        /// <summary>
        /// display all order history of a customer
        /// </summary>

        //public async Task<string> orderHistoryByCustomer()
        //{
        //    IEnumerable<Order>? allRecords = null;
        //    if (_repository is not null) allRecords = await _repository.orderHistoryByCustomerAsync(this.customerId );

        //    var summary = new StringBuilder();
        //    summary.AppendLine($"Order ID\tStore Name\tTotal\tOrder Date");
        //    summary.AppendLine("---------------------------------------------------------------");
        //    foreach (var record in allRecords)
        //    {
        //        summary.AppendLine($"{record.OrderID}\t\t{record.Store.LocationName} \t${record.Total}\t{record.Ordertime}");
        //    }
        //    summary.AppendLine("---------------------------------------------------------------");

        //    return summary.ToString();



        //}



    }

}
