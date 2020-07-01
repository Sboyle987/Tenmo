using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using TenmoServer.Models;

namespace TenmoClient
{
    class APIService
    {
        private readonly IRestClient client;
        public APIService(string baseUrl, string token)
        {
            client = new RestClient(baseUrl);
            client.Authenticator = new JwtAuthenticator(token);
        }
        
        public decimal GetAccountBalance() // TODO Implement client side
        {
            RestRequest request = new RestRequest("account");
            IRestResponse<List<Account>> response = client.Get<List<Account>>(request);

            CheckError(response);

            List<Account> accounts = response.Data;
            if (accounts == null || accounts.Count == 0)
            {
                return 0;
            }
            return accounts[0].Balance;
        }


        private void CheckError(IRestResponse response)
        {
            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                throw new Exception("Error occured - unable to reach server.");
            }

            if(!response.IsSuccessful)
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new Exception("Authorized is required for this option. Please log in.");
                }
                if (response.StatusCode == HttpStatusCode.Forbidden)
                {
                    throw new Exception("You do not have permission to perform the requested action");
                }

                throw new Exception($"Error occurred - received non-success response: {response.StatusCode} ({(int)response.StatusCode})");
            }
        }
    }
}
