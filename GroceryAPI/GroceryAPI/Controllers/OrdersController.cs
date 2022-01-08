using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace GroceryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {

        public OrdersController(IRepository repository)
        {
            _repository = repository;
        }
        private readonly IRepository _repository;

        [HttpPost]
        public async Task<ActionResult<int>> AddNewOrderAsync(Order NewOrder)
        {
            try
            {
                int result= await _repository.AddNewOrderAsync(NewOrder);
                return result;
            }
            catch (SqlException ex)
            {
                return 0;
            }
            
        }



        [HttpGet]
        public async Task<ActionResult<Order>> SearchOrderByIdAsync([FromQuery, Required] int orderid)
        {
            //if (string.IsNullOrEmpty(player))
            //{
            //    return BadRequest("a player is required");
            //}
            // ^ not needed- model validation can handle it!
            // more complex validation
            Order OrderExists = null;
            try
            {
                OrderExists = await _repository.SearchOrderByIdAsync(orderid);
            }
            catch (SqlException ex)
            {
                //   _logger.LogError(ex, "sql error while getting rounds of {player}", player);
                return StatusCode(500);
            }

            return OrderExists;
        }

        [HttpGet]
        [Route("HistoryByCustomer")]
        public async Task<ActionResult<IEnumerable<Order>>> orderHistoryByCustomerAsync([FromQuery, Required] int customerid)
        {

            IEnumerable<Order> oOrders = null;

            try
            {
                oOrders = await _repository.orderHistoryByCustomer(customerid);
            }
            catch (SqlException ex)
            {
                // bad! the exception is not logged, and asp.net was going to log it and return 500 anyway
                // (^ when the catch block just returned 500)
                // you only need to catch exceptions where either you want to do something besides a 500 error
                //     or you want to log the exception with some more context
                //                _logger.LogError(ex, "sql error while getting rounds of {player}", player);
                return StatusCode(500);
            }

            return oOrders.ToList();

        }



        [HttpGet]
        [Route("HistoryByStore")]
        public async Task<ActionResult<IEnumerable<Order>>> orderHistoryByStoreAsync(int locationid)
        {

            IEnumerable<Order> oOrders=null;

            try
            {
                oOrders = await _repository.orderHistoryByStoreAsync(new Stores(locationid));
            }
            catch (SqlException ex)
            {
                // bad! the exception is not logged, and asp.net was going to log it and return 500 anyway
                // (^ when the catch block just returned 500)
                // you only need to catch exceptions where either you want to do something besides a 500 error
                //     or you want to log the exception with some more context
                //                _logger.LogError(ex, "sql error while getting rounds of {player}", player);
                return StatusCode(500);
            }

            return oOrders.ToList();
        }




    }
}
