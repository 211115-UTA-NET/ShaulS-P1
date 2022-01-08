using Xunit;
using GroceryAPI;
using GroceryAPI.Controllers;
using Moq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace GroceryAPITest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {

        }




        [Fact]
        public async Task SearchCustomersByNameAsyncTest()
        {
            Mock<IRepository> mockRepo = new();
            CustomersController a=new CustomersController(mockRepo.Object);
            var result = await a.SearchCustomersByNameAsync("", "");
            var expected = "0";
            Assert.Equal(expected: expected, actual: result.Value.ToString());		


        }


        [Fact]
        public async Task AddNewCustomerAsyncTest()
        {

            Mock<IRepository> mockRepo = new();
            CustomersController a = new CustomersController(mockRepo.Object);
            Customer customer1 = new Customer();
            var result = await a.AddNewCustomerAsync(customer1);
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);



        }



    }
}