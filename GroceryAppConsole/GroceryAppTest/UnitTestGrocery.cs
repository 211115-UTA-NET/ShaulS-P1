using Xunit;
using Moq;
using GroceryAppConsole;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace groceryTest
{
    public class UnitTestGrocery
    {

        [Fact]
        public void TestOrder_Program_IsAllLetters()
        {
            var result = Program.IsAllLetters("shaul8");
            Assert.Equal(expected: false, actual: result);

        }
        [Fact]
        public void TestOrder_Program_IsIntegerNegetiveNumber_succeed()
        {

            var result = Program.IsInteger("-1");
            Assert.Equal(expected: 0, actual: result);

        }



        [Fact]
        public void TestOrder_Program_IsInteger_succeed()
        {

            var result = Program.IsInteger("a");
            Assert.Equal(expected: 0, actual: result);

        }

        [Fact]
        public void TestOrder_Program_IsInteger_fail()
        {
            var result = Program.IsInteger("1");
            Assert.Equal(expected: 1, actual: result);
        }


        [Fact]
        public void TestOrder_DisplayDetailsOrder_Correctformat()
        {
            DateTime DateTime1 = new DateTime(2021, 1, 1);
            Order orderTest = new(new(), new(), DateTime1);            
            //string summery = "";
            //act
            var result = orderTest.DisplayDetailsOrder();
            //var a = false;
            // ASSERT (checking that the behavior was as expected)
            var expected = "Store Name:\r\nOrder Date:01/01/2021 00:00:00\t\tCustomer Name:  \t\t\r\nProduct\t\tQuantity\t\tPrice\tAmount\r\n---------------------------------------------------------------\r\n---------------------------------------------------------------\r\nOrder Total\t$0 \r\n";
            Assert.Equal(expected: expected, actual: result);


        }
        [Fact]
        public async Task DisplayStoreList_Correctformat()
        {
            //public async static Task<string> DisplayStoreList()

            //Stores s = new("Test");
            Mock<IGroceryAPI> mockRepo = new();
            Mock<Stores> TestStoreMock = new();
            Stores.Repository = mockRepo.Object;

            //List<Stores>
            List<Stores> a = new ();
            mockRepo.Setup(x => x.GetStoreListAsync()).Returns(Task.FromResult(a));
            //mockRepo.Setup(x => x.GetStoreListAsync()).Returns(Task.FromResult(Array.Empty<Stores>()));
            // act
            string result = await Stores.DisplayStoreList();
            // assert
            var expected = "Store ID\tStore Name\t\r\n---------------------------------------------------------------\r\n---------------------------------------------------------------\r\n";
            Assert.Equal(expected: expected, actual: result);


        }


        //[Fact]
        //public void TestSales_orderHistoryByStore_Correctformat()
        //{
        //    // arrange
        //    Stores s = new("Test");
        //    Mock<IRepository> mockRepo = new();
        //    Mock<Stores> TestStoreMock = new();
        //    Stores.Repository = mockRepo.Object;
        //    mockRepo.Setup(x => x.orderHistoryByStore(TestStoreMock.Object)).Returns(Array.Empty<Order>());
        //    // act
        //    string result = s.orderHistoryByStore();
        //    // assert
        //    var expected = "Order ID\tCustomer Name\tTotal\tOrder Date\r\n---------------------------------------------------------------\r\n---------------------------------------------------------------\r\n";
        //    Assert.Equal(expected: expected, actual: result);


        //}

        //[Fact]
        //public void TestCustomer_orderHistoryByCustomer_Correctformat()
        //{
        //    // arrange
        //    Customer customerTest = new("FirstName","LastName");
        //    Mock<IRepository> mockRepo = new();
        //    Mock<Customer> TestCustomerMock = new();
        //    Customer.Repository = mockRepo.Object;
        //    mockRepo.Setup(x => x.orderHistoryByCustomer(TestCustomerMock.Object)).Returns(Array.Empty<Order>());
        //    // act
        //    string result = customerTest.orderHistoryByCustomer();
        //    // assert
        //    var expected = "Order ID\tStore Name\tTotal\tOrder Date\r\n---------------------------------------------------------------\r\n---------------------------------------------------------------\r\n";
        //    Assert.Equal(expected: expected, actual: result);


        //}

        [Fact]
        public void TestOrder_CheckOrderValidQuantity_fail()
        {
            
            Order orderTest = new(new("firstName","LastName"),new());
            orderTest.OrdersLines.Add(new(new(), Order.MaxQuantity+1));
            string summery = "";
            var result = orderTest.CheckOrderValidQuantity(ref summery);
            //var a = false;
            // ASSERT (checking that the behavior was as expected)
            Assert.Equal(expected: false, actual: result);
        

        }

        [Fact]
        public void TestOrder_CheckOrderValidQuantity_succeed()
        {

            Order orderTest = new(new("firstName", "LastName"), new());
            orderTest.OrdersLines.Add(new(new(), 10));
            string summery = "";
            var result = orderTest.CheckOrderValidQuantity(ref summery);            
            // ASSERT (checking that the behavior was as expected)
            Assert.Equal(expected: true, actual: result);


        }


    }
}