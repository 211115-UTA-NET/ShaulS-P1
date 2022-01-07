using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace GroceryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class CustomersController : ControllerBase
    {

        public CustomersController(IRepository repository)
        {
            _repository = repository;
        }




        private readonly IRepository _repository;
        
        // GET /api/customers/?firstname=shaul&lastname=stavi
        [HttpGet]
        public async Task<ActionResult<int>> SearchCustomersByNameAsync([FromQuery, Required] string firstname, [FromQuery, Required] string lastname)
        {
            //if (string.IsNullOrEmpty(player))
            //{
            //    return BadRequest("a player is required");
            //}
            // ^ not needed- model validation can handle it!
            // more complex validation
            int  CustomerExists=0;
            try
            {
                CustomerExists = await _repository.SearchCustomersByNameAsync(new(firstname, lastname));
            }
            catch (SqlException ex)
            {
             //   _logger.LogError(ex, "sql error while getting rounds of {player}", player);
                return StatusCode(500);
            }

            return CustomerExists;
        }


        [HttpPost]
        public async Task<IActionResult> AddNewCustomerAsync(Customer NewCustomer)
        {


            try
            {
                await _repository.AddNewCustomerAsync(NewCustomer);
            }
            catch (SqlException ex)
            {
                return StatusCode(500);
            }
            return Ok();
        }



    }
}
