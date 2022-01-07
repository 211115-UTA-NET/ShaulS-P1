using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryAppConsole
{
    /// <summary>
    /// Product Record Update and insert data from Database
    /// </summary>
    public class Product
    {
        int productId;
        string productName="";
        double productPrice;
        public Product()
        {
        }
        public Product(string productName)
        {
            this.productName = productName;
        }

        public int ProductId { get => productId; set => productId = value; }
        public string ProductName { get => productName; set => productName = value; }
        public double ProductPrice { get => productPrice; set => productPrice = value; }
        private static IGroceryAPI? _repository;
        public static IGroceryAPI? Repository { get => _repository; set => _repository = value; }
        /// <summary>
        /// Search Product By Name
        /// </summary>

        //public async Task<Product> SearchProductByNameAsync(string ProductName)
        public async Task<Product> SearchProductByName()
        {
            Product tmpProduct= await _repository.SearchProductByNameAsync(this.ProductName);
//            if (tmp != null)
//
  //              this = tmp;
            return tmpProduct; 
                //_repository is null ? false : await _repository.SearchProductByNameAsync(this.ProductName);
            //return false;
        }

    }

}
