using System;
using System.Text;

namespace GroceryAppConsole
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
        private static IGroceryAPI? _repository;
        public static IGroceryAPI? Repository { get => _repository; set => _repository = value; }

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
        public async void AddNewCustomer()
        {
            if (_repository is not null) await _repository.SubmitCustomerAsync(this);
        }
        public async  Task<int> SearchCustomersByNameAsync()
        {
            //return false;
            int Result= await _repository.SearchCustomersByNameAsync(this.FirstName, this.LastName);
            return Result;
            //return Result is null ? false : (bool)Result;
            //_repository is null ? false :

        }
        /// <summary>
        /// display all order history of a customer
        /// </summary>

        public async Task<string> orderHistoryByCustomer()
        {
            IEnumerable<Order>? allRecords = null;
            if (_repository is not null) allRecords = await _repository.orderHistoryByCustomerAsync(this.CustomerId);

            var summary = new StringBuilder();
            summary.AppendLine($"Order ID\tStore Name\tTotal\tOrder Date");
            summary.AppendLine("---------------------------------------------------------------");
            foreach (var record in allRecords)
            {
                summary.AppendLine($"{record.OrderID}\t\t{record.Store.LocationName} \t${record.Total}\t{record.Ordertime}");
            }
            summary.AppendLine("---------------------------------------------------------------");

            return summary.ToString();



        }



    }

}
