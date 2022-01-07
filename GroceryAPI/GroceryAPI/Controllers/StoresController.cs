using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
namespace GroceryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoresController : ControllerBase
    {

        public StoresController(IRepository repository)
        {
            _repository = repository;
        }




        private readonly IRepository _repository;

        // GET /api/stores/?firstname=shaul&lastname=stavi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Stores>>> GetStoresAsync()
        {
            IEnumerable<Stores> oStores;

            try
            {
                oStores = await _repository.DisplayStoreList();
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

            return oStores.ToList();

            //if (string.IsNullOrEmpty(player))
            //    //{
            //    //    return BadRequest("a player is required");
            //    //}
            //    // ^ not needed- model validation can handle it!
            //    // more complex validation
            //    bool CustomerExists = false;
            //    try
            //    {
            //        CustomerExists = await _repository.SearchCustomersByNameAsync(new(firstname, lastname));
            //    }
            //    catch (SqlException ex)
            //    {
            //        //   _logger.LogError(ex, "sql error while getting rounds of {player}", player);
            //        return StatusCode(500);
            //    }

            //    return CustomerExists;
            //}

        }

        }
}
