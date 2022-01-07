using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace GroceryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        public ProductsController(IRepository repository)
        {
            _repository = repository;
        }




        private readonly IRepository _repository;

        // GET /api/Products/?firstname=shaul&lastname=stavi
        [HttpGet]                                               
        public async Task<ActionResult<Product>> SearchProductByNameAsync([FromQuery, Required] string productname)
        {
            //if (string.IsNullOrEmpty(player))
            //{
            //    return BadRequest("a player is required");
            //}
            // ^ not needed- model validation can handle it!
            // more complex validation
            Product ProductExists = null;
            try
            {
                ProductExists = await _repository.SearchProductByNameAsync(new(productname));
            }
            catch (SqlException ex)
            {
                //   _logger.LogError(ex, "sql error while getting rounds of {player}", player);
                return StatusCode(500);
            }

            return ProductExists;
        }

    }
}
