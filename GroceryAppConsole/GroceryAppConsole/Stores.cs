using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryAppConsole
{
    /// <summary>
    /// Stores Record Update and insert data from Database
    /// </summary>
    public class Stores
    {
        string locationName="";

        public Stores()
        { }
        public Stores(string locationName)
        {
            this.locationName = locationName;
        }
        public Stores(int LocationId)
        {
            this.LocationId = LocationId;
        }
        public Stores(string locationName, int LocationId)
        {
            this.locationName = locationName;
            this.LocationId = LocationId;
        }
        int locationId;
        private static IGroceryAPI? _repository;
        public static IGroceryAPI? Repository { get => _repository; set => _repository = value; }

        public string LocationName { get => locationName; set => locationName = value; }
        public int LocationId { get => locationId; set => locationId = value; }

        public bool SearchStoreByName()
        {
            return false;
//            return _repository is null ? false : _repository.SearchStoreByName(this);
        }
        public async Task<string> orderHistoryByStore()
        {
            IEnumerable<Order>? allRecords = null;
            if (_repository is not null) allRecords = await _repository.orderHistoryByStoreAsync(this.LocationId);

            var summary = new StringBuilder();
            summary.AppendLine($"Order ID\tCustomer Name\tTotal\tOrder Date");
            summary.AppendLine("---------------------------------------------------------------");
            foreach (var record in allRecords)
            {
                summary.AppendLine($"{record.OrderID}\t\t{record.Customer.FirstName} {record.Customer.LastName}\t${record.Total}\t{record.Ordertime}");
            }
            summary.AppendLine("---------------------------------------------------------------");

            return summary.ToString();



        }
        public async static Task<string> DisplayStoreList()
        {
            IEnumerable<Stores>? allRecords = null;
            if (_repository is not null) allRecords = await _repository.GetStoreListAsync();

            var summary = new StringBuilder();
            summary.AppendLine($"Store ID\tStore Name\t");
            summary.AppendLine("---------------------------------------------------------------");
            foreach (var record in allRecords)
            {
                summary.AppendLine($"{record.LocationId}\t{record.LocationName}");
            }
            summary.AppendLine("---------------------------------------------------------------");

            return summary.ToString();

        }
    }
}
