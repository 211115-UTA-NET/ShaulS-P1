using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryAPI
{
/// <summary>
/// methods for connecting and getting data from and to the database
/// </summary>
    class SqlRepository : IRepository
    {

        private readonly string _connectionString;

        public SqlRepository(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }


        public async Task  AddNewCustomerAsync(Customer NewCustomer)
        {
            try
            {

                using SqlConnection connection = new(_connectionString);
                await connection.OpenAsync();
                string cmdText = @"INSERT INTO dbo.Customer (FirstName,LastName) VALUES (@FirstName,@LastName);
                                SELECT CustomerID FROM dbo.Customer WHERE FirstName = @FirstName and LastName =@LastName ;";
                using SqlCommand cmd = new(cmdText, connection);
                cmd.Parameters.AddWithValue("@FirstName", NewCustomer.FirstName);
                cmd.Parameters.AddWithValue("@LastName", NewCustomer.LastName);
                using SqlDataReader reader = cmd.ExecuteReader();

                reader.Read();
                NewCustomer.CustomerId = reader.GetInt32(0);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
        public bool AddNewOrder(Order NewOrder)
        {
            try
            {
                using SqlConnection connection = new(_connectionString);
                connection.Open();
                string cmdText = @"INSERT INTO dbo.StoreOrder (CustomerID,LocationId,orderTime) VALUES (@CustomerID,@LocationId,@orderTime);
                                    SELECT SCOPE_IDENTITY() ;";
                using SqlCommand cmd = new(cmdText, connection);
                cmd.Parameters.AddWithValue("@CustomerID", NewOrder.Customer.CustomerId);
                cmd.Parameters.AddWithValue("@LocationId", NewOrder.Store.LocationId);
                cmd.Parameters.AddWithValue("@orderTime", NewOrder.Ordertime);
                using SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                int OrderId = Convert.ToInt32(reader[0]);
                reader.Close();
                //reader.GetInt32(0);
                NewOrder.OrderID = OrderId;
                double sum = 0;

                cmdText = @"INSERT INTO dbo.StoreOrderItem (OrderID,ProductID,Quantity,Price) VALUES (@OrderID,@ProductID,@Quantity,@Price);";

                foreach (var record in NewOrder.OrdersLines)
                {
                    using (SqlCommand cmd2 = new(cmdText, connection))
                    {
                        cmd2.Parameters.AddWithValue("@OrderID", OrderId);
                        cmd2.Parameters.AddWithValue("@ProductID", record.OrderProduct.ProductId);
                        cmd2.Parameters.AddWithValue("@Quantity", record.Quantity);
                        cmd2.Parameters.AddWithValue("@Price", record.OrderPrice);
                        sum += record.Quantity * record.OrderPrice;
                        cmd2.ExecuteNonQuery();
                        cmd2.Parameters.Clear();
                    }
                }
                cmdText = @"Update dbo.StoreOrder set Total=@Total where OrderID=@OrderID;";
                using SqlCommand cmd3 = new(cmdText, connection);
                cmd3.Parameters.AddWithValue("@OrderID", OrderId);
                cmd3.Parameters.AddWithValue("@Total", sum);
                cmd3.ExecuteNonQuery();
                cmdText = @"dbo.UpdateInventory";
                using SqlCommand cmd4 = new(cmdText, connection);
                cmdText = @"dbo.UpdateInventory";
                cmd4.CommandType = System.Data.CommandType.StoredProcedure;
                cmd4.Parameters.AddWithValue("@OrderID", OrderId);
                cmd4.Parameters.Add("@Ret", System.Data.SqlDbType.NVarChar, 100).Direction = System.Data.ParameterDirection.Output;




                cmd4.ExecuteNonQuery();
                //cmd.Parameters["@RETURN_VALUE"].Value
                string rtn = (string)cmd4.Parameters["@Ret"].Value;
                if (rtn == "1")
                    return true;
                else
                {
                    Console.WriteLine(rtn);
                    cmdText = @"delete dbo.StoreOrder where OrderID=@OrderID;";
                    using SqlCommand cmd5 = new(cmdText, connection);
                    cmd5.Parameters.AddWithValue("@OrderID", OrderId);
                    cmd5.ExecuteNonQuery();
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return false;
        }



        public async Task<bool> SearchCustomersByNameAsync(Customer findCustomer)
        {
            try
            {
                using SqlConnection connection = new(_connectionString);
                await connection.OpenAsync();

                using SqlCommand cmd = new(
                    @"SELECT CustomerId
                  FROM dbo.customer
                  WHERE FirstName = @FirstName and LastName=@LastName ",
                    connection);

                cmd.Parameters.AddWithValue("@FirstName", findCustomer.FirstName);
                cmd.Parameters.AddWithValue("@LastName", findCustomer.LastName);
                using SqlDataReader reader = cmd.ExecuteReader();
                bool Result = false;
                if (reader.Read())
                {
                    Result = true;
                    findCustomer.CustomerId = reader.GetInt32(0);
                }
                connection.Close();
                return Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return false;
        }

        public bool SearchProductByName(Product findProduct)
        {
            try
            {
                using SqlConnection connection = new(_connectionString);
                connection.Open();

                using SqlCommand cmd = new(
                    @"SELECT ProductId,DefaultPrice 
                  FROM dbo.Product
                  WHERE ProductName = @ProductName",
                    connection);

                cmd.Parameters.AddWithValue("@ProductName", findProduct.ProductName);
                using SqlDataReader reader = cmd.ExecuteReader();
                bool Result = false;
                if (reader.Read())
                {
                    Result = true;
                    findProduct.ProductId = reader.GetInt32(0);
                    findProduct.ProductPrice = reader.GetDouble(1);
                }
                connection.Close();
                return Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return false;
        }
        public IEnumerable<Order> orderHistoryByStore(Stores FindStore)
        {
            try
            {
                List<Order> result = new();
                using SqlConnection connection = new(_connectionString);
                connection.Open();
                using SqlCommand cmd = new(
                    @"SELECT StoreOrder.*,Customer.FirstName,Customer.LastName FROM dbo.StoreOrder inner join dbo.Customer on StoreOrder.Customerid=Customer.Customerid
                where StoreOrder.LocationId=@LocationId and IsApproved<>0",
                    connection);
                cmd.Parameters.AddWithValue("@LocationId", FindStore.LocationId);
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new(new((string)reader["FirstName"], (string)reader["LastName"]), FindStore, (DateTime)reader["OrderTime"], (double)reader["Total"], (int)reader["OrderID"]));
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return null;
        }

        public IEnumerable<Order> orderHistoryByCustomer(Customer FindCustomer)
        {
            try
            {
                List<Order> result = new();
                using SqlConnection connection = new(_connectionString);
                connection.Open();
                using SqlCommand cmd = new(
                    @"SELECT StoreOrder.*,Customer.FirstName,Customer.LastName,StoreLocation.LocationName FROM dbo.StoreOrder inner join dbo.Customer on StoreOrder.Customerid=Customer.Customerid
                                                                                               inner join dbo.StoreLocation on StoreOrder.LocationId=StoreLocation.LocationId                          
                where StoreOrder.CustomerId=@CustomerId and IsApproved<>0",
                    connection);
                cmd.Parameters.AddWithValue("@CustomerId", FindCustomer.CustomerId);
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new(new((string)reader["FirstName"], (string)reader["LastName"]), new((string)reader["LocationName"]), (DateTime)reader["OrderTime"], (double)reader["Total"], (int)reader["OrderID"]));
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return null;
        }

        public IEnumerable<Stores> DisplayStoreList()
        {
            try
            {
                List<Stores> result = new();
                using SqlConnection connection = new(_connectionString);
                connection.Open();
                using SqlCommand cmd = new(
                    @"SELECT *                   FROM dbo.StoreLocation",
                    connection);
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new((string)reader["LocationName"], (int)reader["LocationId"]));
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return null;
        }
        public bool SearchStoreByName(Stores findStore)
        {
            try
            {
                using SqlConnection connection = new(_connectionString);
                connection.Open();

                using SqlCommand cmd = new(
                    @"SELECT LocationId 
                  FROM dbo.StoreLocation
                  WHERE LocationName = @LocationName",
                    connection);

                cmd.Parameters.AddWithValue("@LocationName", findStore.LocationName);
                using SqlDataReader reader = cmd.ExecuteReader();
                bool Result = false;
                if (reader.Read())
                {
                    Result = true;
                    findStore.LocationId = reader.GetInt32(0);
                }
                connection.Close();
                return Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return false;
        }


        public Order SearchOrderById(int OrderId)
        {
            try
            {
                using SqlConnection connection = new(_connectionString);
                connection.Open();

                using SqlCommand cmd = new(
                    @"SELECT StoreOrder.*,StoreOrderItem.*,StoreLocation.*,Product.ProductName,Customer.*
                  FROM dbo.StoreOrder inner join dbo.StoreOrderItem on StoreOrder.OrderID=StoreOrderItem.OrderID
                                 inner join dbo.StoreLocation on StoreLocation.LocationId=StoreOrder.LocationId
                                 inner join dbo.Product on Product.ProductID=StoreOrderItem.ProductID
                                 inner join dbo.Customer on Customer.CustomerId=StoreOrder.CustomerId
                  WHERE StoreOrder.OrderId = @OrderId",
                    connection);

                cmd.Parameters.AddWithValue("@OrderId", OrderId);
                using SqlDataReader reader = cmd.ExecuteReader();
                bool Result = false;
                Order tempOrder = null;
                Customer customer = null;
                Stores stores = null;
                bool FirstLine = true;
                while (reader.Read())
                {
                    if (FirstLine)
                    {
                        customer = new((string)reader["FirstName"], (string)reader["LastName"]);
                        stores = new((string)reader["LocationName"]);
                        tempOrder = new(customer, stores, (DateTime)reader["orderTime"], (double)reader["Total"]);
                        FirstLine = false;
                    }
                    tempOrder.OrdersLines.Add(new(new((string)reader["ProductName"]), (int)reader["Quantity"], (double)reader["Price"]));
                }

                return tempOrder;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return null;

        }



    }

}
