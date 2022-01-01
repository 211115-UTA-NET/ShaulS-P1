using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryAPI
{
    /// <summary>
    /// Product Record Update and insert data from Database
    /// </summary>
    public class Product
    {
        int productId;
        string productName;
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
        private static IRepository? _repository;
        public static IRepository? Repository { get => _repository; set => _repository = value; }
        /// <summary>
        /// Search Product By Name
        /// </summary>

        public bool SearchProductByName()
        {
            return _repository is null ? false : _repository.SearchProductByName(this);
        }

    }

}
