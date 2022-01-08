using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryAPI
{
    /// <summary>
    /// Use Interface to sperate sql operations from Method done in Local Program
    /// </summary>
    public interface IRepository
    {
        
        Task AddNewCustomerAsync(Customer NewCustomer);
        //bool SearchCustomersByName(Customer NewCustomer);
        public Task<int> SearchCustomersByNameAsync(Customer findCustomer);
        Task<Product> SearchProductByNameAsync(Product NewProduct);

        bool SearchStoreByName(Stores NewStore);
        //void Save();
        Task<Order> SearchOrderByIdAsync(int OrderId);
        public Task<int> AddNewOrderAsync(Order NewOrder);
        
        Task<IEnumerable<Stores>> DisplayStoreList();
        Task<IEnumerable<Order>> orderHistoryByStoreAsync(Stores FindStore);        
        Task<IEnumerable<Order>> orderHistoryByCustomer(int FindCustomer);
    }

}
