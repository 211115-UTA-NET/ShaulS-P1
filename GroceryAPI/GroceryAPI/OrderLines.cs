using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryAPI
{
    /// <summary>
    /// Order Line Record Update and insert data from Database
    /// </summary>
    public class OrderLines
    {
        Product orderProduct;
        double orderPrice;
        int quantity;
        public OrderLines(Product orderProduct, int quantity)
        {
            this.orderProduct = orderProduct;
            this.quantity = quantity;
            this.orderPrice = orderProduct.ProductPrice;
        }
        public OrderLines(Product orderProduct, int quantity, double orderPrice)
        {
            this.orderProduct = orderProduct;
            this.quantity = quantity;
            this.orderPrice = orderPrice;
        }


        public double OrderPrice { get => orderPrice; set => orderPrice = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public Product OrderProduct { get => orderProduct; set => orderProduct = value; }
    }
}
