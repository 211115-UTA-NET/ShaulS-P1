using GroceryAppConsole.Exceptions;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace GroceryAppConsole
{
    public class GroceryAPI : IGroceryAPI
    {
        private readonly HttpClient _httpClient = new();
        public GroceryAPI(Uri serverUri)
        {
            // add some default headers for any HttpRequestMessage going through this HttpClient
            //s_httpClient.DefaultRequestHeaders.Accept.Add(new(MediaTypeNames.Application.Json));
            // you can give the httpclient a default base URL.
            // then the URL you give it for each individual request can be "relative URLs"
            _httpClient.BaseAddress = serverUri;
        }
        //AddNewCustomer

        public async Task<HttpStatusCode> SubmitCustomerAsync(Customer NewCustomer)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(
               "api/customers", NewCustomer);

            return response.StatusCode;

        }




        public async Task<int> SubmitOrderAsync(Order NewOrder)
        {            
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/Orders", NewOrder);
            var rounds = await response.Content.ReadFromJsonAsync<int>();
            if (rounds == 0)
            {
                throw new UnexpectedServerBehaviorException();
            }

            return rounds;

        }


        public async Task<List<Stores>> GetStoreListAsync()
        {
            // if you want to add any headers to the request, then can't use the HttpClient.GetAsync, PostAsync, PutAsync, etc.
            //    much less these GetFromJsonAsync extension methods.
            // instead, you need to construct a HttpRequestMessage, then call HttpClient.SendAsync.

            // even more restful would be to build this URL using some other response's hypermedia values
            // e.g.: starting point would be GET/POST a player, that response would have
            //    hypermedia links to... get all the rounds of that player, add a new round for that player
            // (HATEOAS) - effort into HATEOAS is not really required for P1 but it would be cool

            // better, more secure way to insert a string into a url's query string
            // with appropriate URL-encoding
            //Dictionary<string, string> query = new() { ["player"] = name };
            string requestUri = "/api/Stores";

            HttpRequestMessage request = new(HttpMethod.Get, requestUri);
            // telling the server we expect application/json reply. ("content negotiation" in http/rest)
            request.Headers.Accept.Add(new(MediaTypeNames.Application.Json));

            HttpResponseMessage response;
            try
            {
                response = await _httpClient.SendAsync(request);
            }
            catch (HttpRequestException ex)
            {
                throw new UnexpectedServerBehaviorException("network error", ex);
            }

            response.EnsureSuccessStatusCode(); // throw if the status code is not 2xx
            //if (response.StatusCode == 401)
            //{
            //    throw new UnauthorizedException();
            //}

            if (response.Content.Headers.ContentType?.MediaType != MediaTypeNames.Application.Json)
            {
                throw new UnexpectedServerBehaviorException();
            }

            var rounds = await response.Content.ReadFromJsonAsync<List<Stores>>();
            if (rounds == null)
            {
                throw new UnexpectedServerBehaviorException();
            }

            return rounds;
        }



        


        public async Task<Order> SearchOrderByIdAsync(int OrderID)
        {
            Dictionary<string, string> query = new() { ["OrderID"] = OrderID.ToString() };
            string requestUri = QueryHelpers.AddQueryString("/api/Orders", query);

            HttpRequestMessage request = new(HttpMethod.Get, requestUri);
            // telling the server we expect application/json reply. ("content negotiation" in http/rest)
            request.Headers.Accept.Add(new(MediaTypeNames.Application.Json));

            HttpResponseMessage response;
            try
            {
                response = await _httpClient.SendAsync(request);
            }
            catch (HttpRequestException ex)
            {
                throw new UnexpectedServerBehaviorException("network error", ex);
            }

            response.EnsureSuccessStatusCode(); // throw if the status code is not 2xx
                                                //if (response.StatusCode == 401)
                                                //{
                                                //    throw new UnauthorizedException();
                                                //}

            //if (response.Content.Headers.ContentType?.MediaType != MediaTypeNames.Application.Json)
            //{
            //    throw new UnexpectedServerBehaviorException();
            //}
            Order rounds = null;
            if (response.StatusCode != HttpStatusCode.NoContent )
            { 
                rounds = await response.Content.ReadFromJsonAsync<Order>();
            }
                //if (rounds is null)
            //{
        //        throw new UnexpectedServerBehaviorException();
            //}

            return rounds;
            //return false;

        }

        public async Task<List<Order>> orderHistoryByStoreAsync(int locationid)
        {
            Dictionary<string, string> query = new() { ["locationid"] = locationid.ToString() };
            string requestUri = QueryHelpers.AddQueryString("/api/Orders/HistoryByStore", query);            
            HttpRequestMessage request = new(HttpMethod.Get, requestUri);
            // telling the server we expect application/json reply. ("content negotiation" in http/rest)
            request.Headers.Accept.Add(new(MediaTypeNames.Application.Json));

            HttpResponseMessage response;
            try
            {
                response = await _httpClient.SendAsync(request);
            }
            catch (HttpRequestException ex)
            {
                throw new UnexpectedServerBehaviorException("network error", ex);
            }

            response.EnsureSuccessStatusCode(); // throw if the status code is not 2xx
                                                //if (response.StatusCode == 401)
                                                //{
                                                //    throw new UnauthorizedException();
                                                //}

            if (response.Content.Headers.ContentType?.MediaType != MediaTypeNames.Application.Json)
            {
                throw new UnexpectedServerBehaviorException();
            }

            var rounds = await response.Content.ReadFromJsonAsync<List<Order>>();
            if (rounds is null)
            {
                throw new UnexpectedServerBehaviorException();
            }

            return rounds;
            //return false;

        }

        
        public async Task<List<Order>> orderHistoryByCustomerAsync(int customerid)
        {
            Dictionary<string, string> query = new() { ["customerid"] = customerid.ToString() };
            string requestUri = QueryHelpers.AddQueryString("/api/Orders/HistoryByCustomer", query);
            HttpRequestMessage request = new(HttpMethod.Get, requestUri);
            // telling the server we expect application/json reply. ("content negotiation" in http/rest)
            request.Headers.Accept.Add(new(MediaTypeNames.Application.Json));

            HttpResponseMessage response;
            try
            {
                response = await _httpClient.SendAsync(request);
            }
            catch (HttpRequestException ex)
            {
                throw new UnexpectedServerBehaviorException("network error", ex);
            }

            response.EnsureSuccessStatusCode(); // throw if the status code is not 2xx
                                                //if (response.StatusCode == 401)
                                                //{
                                                //    throw new UnauthorizedException();
                                                //}

            if (response.Content.Headers.ContentType?.MediaType != MediaTypeNames.Application.Json)
            {
                throw new UnexpectedServerBehaviorException();
            }

            var rounds = await response.Content.ReadFromJsonAsync<List<Order>>();
            if (rounds is null)
            {
                throw new UnexpectedServerBehaviorException();
            }

            return rounds;
            //return false;

        }


        public async Task<Product> SearchProductByNameAsync(string ProductName)
        {
            Dictionary<string, string> query = new() { ["ProductName"] = ProductName};
            string requestUri = QueryHelpers.AddQueryString("/api/Products", query);

            HttpRequestMessage request = new(HttpMethod.Get, requestUri);
            // telling the server we expect application/json reply. ("content negotiation" in http/rest)
            request.Headers.Accept.Add(new(MediaTypeNames.Application.Json));

            HttpResponseMessage response;
            try
            {
                response = await _httpClient.SendAsync(request);
            }
            catch (HttpRequestException ex)
            {
                throw new UnexpectedServerBehaviorException("network error", ex);
            }

            response.EnsureSuccessStatusCode(); // throw if the status code is not 2xx
                                                //if (response.StatusCode == 401)
                                                //{
                                                //    throw new UnauthorizedException();
                                                //}

            if (response.Content.Headers.ContentType?.MediaType != MediaTypeNames.Application.Json)
            {
                throw new UnexpectedServerBehaviorException();
            }

            var rounds = await response.Content.ReadFromJsonAsync<Product>();
            if (rounds is null)
            {
                throw new UnexpectedServerBehaviorException();
            }

            return rounds;
            //return false;

        }

        public async Task<int> SearchCustomersByNameAsync(string FirstName, string LastName)
        {
            Dictionary<string, string> query = new() { ["FirstName"] = FirstName , ["LastName"] = LastName };
            string requestUri = QueryHelpers.AddQueryString("/api/Customers", query);

            HttpRequestMessage request = new(HttpMethod.Get, requestUri);
            // telling the server we expect application/json reply. ("content negotiation" in http/rest)
            request.Headers.Accept.Add(new(MediaTypeNames.Application.Json));

            HttpResponseMessage response;
            try
            {
                response = await _httpClient.SendAsync(request);
            }
            catch (HttpRequestException ex)
            {
                throw new UnexpectedServerBehaviorException("network error", ex);
            }

            response.EnsureSuccessStatusCode(); // throw if the status code is not 2xx
                                                //if (response.StatusCode == 401)
                                                //{
                                                //    throw new UnauthorizedException();
                                                //}

            if (response.Content.Headers.ContentType?.MediaType != MediaTypeNames.Application.Json)
            {
                throw new UnexpectedServerBehaviorException();
            }

            var rounds = await response.Content.ReadFromJsonAsync<int>();
//            if (rounds == 0)
  //          {
    //            throw new UnexpectedServerBehaviorException();
      //      }

            return rounds;
            //return false;

        }
//        public async Task<bool> GetRoundsOfPlayerAsync(string name)
//{
//    // if you want to add any headers to the request, then can't use the HttpClient.GetAsync, PostAsync, PutAsync, etc.
//    //    much less these GetFromJsonAsync extension methods.
//    // instead, you need to construct a HttpRequestMessage, then call HttpClient.SendAsync.

//    // even more restful would be to build this URL using some other response's hypermedia values
//    // e.g.: starting point would be GET/POST a player, that response would have
//    //    hypermedia links to... get all the rounds of that player, add a new round for that player
//    // (HATEOAS) - effort into HATEOAS is not really required for P1 but it would be cool

//    // better, more secure way to insert a string into a url's query string
//    // with appropriate URL-encoding
//    Dictionary<string, string> query = new() { ["player"] = name };
//    string requestUri = QueryHelpers.AddQueryString("/api/rounds", query);

//    HttpRequestMessage request = new(HttpMethod.Get, requestUri);
//    // telling the server we expect application/json reply. ("content negotiation" in http/rest)
//    request.Headers.Accept.Add(new(MediaTypeNames.Application.Json));

//    HttpResponseMessage response;
//    try
//    {
//        response = await _httpClient.SendAsync(request);
//    }
//    catch (HttpRequestException ex)
//    {
//        throw new UnexpectedServerBehaviorException("network error", ex);
//    }

//    response.EnsureSuccessStatusCode(); // throw if the status code is not 2xx
//                                        //if (response.StatusCode == 401)
//                                        //{
//                                        //    throw new UnauthorizedException();
//                                        //}

//    if (response.Content.Headers.ContentType?.MediaType != MediaTypeNames.Application.Json)
//    {
//        throw new UnexpectedServerBehaviorException();
//    }

//    var rounds = await response.Content.ReadFromJsonAsync<List<Round>>();
//    if (rounds == null)
//    {
//        throw new UnexpectedServerBehaviorException();
//    }

//    return rounds;
//}


        
    }
}
