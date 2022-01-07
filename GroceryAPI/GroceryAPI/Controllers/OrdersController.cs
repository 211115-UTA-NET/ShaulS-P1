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



    }
}
