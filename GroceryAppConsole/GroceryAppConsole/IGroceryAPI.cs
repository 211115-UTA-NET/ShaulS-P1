using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GroceryAppConsole
{
    public interface IGroceryAPI
    {
        
        Task<Product> SearchProductByNameAsync(string ProductName);

        Task<Order> SearchOrderByIdAsync(int OrderID);
        Task<int> SearchCustomersByNameAsync(string FirstName, string LastName);
        Task<HttpStatusCode> SubmitCustomerAsync(Customer NewCustomer);
        Task<List<Stores>> GetStoreListAsync();
        Task<int> SubmitOrderAsync(Order NewOrder);
        Task<List<Order>> orderHistoryByStoreAsync(int locationid);
        Task<List<Order>> orderHistoryByCustomerAsync(int customerid);

    }
}
