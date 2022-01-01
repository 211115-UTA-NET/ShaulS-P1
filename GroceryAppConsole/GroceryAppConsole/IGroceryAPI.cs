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
        Task<bool?> SearchCustomersByNameAsync(string FirstName, string LastName);
        Task<HttpStatusCode> SubmitCustomerAsync(Customer NewCustomer);
    }
}
