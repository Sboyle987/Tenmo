using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Net;
using System.Text;
using TenmoClient.Models;

namespace TenmoClient
{
    public class APIService
    {
        //private readonly static string API_Base_Url = "https://localhost:44315/";
        private readonly IRestClient client;
        public APIService(string baseUrl, string token)
        {
            client = new RestClient(baseUrl);
            client.Authenticator = new JwtAuthenticator(token);
        }
        
        public decimal GetAccountBalance()
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
        public List<Account> GetAccounts() 
        {
            List<Account> accounts = new List<Account>();
            RestRequest request = new RestRequest("transfer");
            IRestResponse<List<Account>> response = client.Get<List<Account>>(request);

            CheckError(response);
            
            accounts = response.Data;
            
            return accounts;
        }
        public Transfer TransferMoney(Transfer transfer)
        {
            RestRequest request = new RestRequest("transfer/exchange");
            request.AddJsonBody(transfer);
            IRestResponse<Transfer> response = client.Post<Transfer>(request);
            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                Console.WriteLine("An error occured connecting with the server");
                return null;
            }
            else if (!response.IsSuccessful)
            {
                Console.WriteLine($"An error occured - Insufficient Funds");
            }
            return response.Data;
        }
        public List<Transfer> GetTransfers()//this needs to take an account bc we get the transfers of the current user
        {
            RestRequest request = new RestRequest("transfer/list");
            IRestResponse<List<Transfer>> response = client.Get<List<Transfer>>(request);

            CheckError(response);

            List<Transfer> transfers = response.Data;

            return transfers;
        }
        public Transfer GetTransferById(int transferId)
        {
             {
                RestRequest request = new RestRequest("transfer/" + transferId);
                IRestResponse<Transfer> response = client.Get<Transfer>(request);

                CheckError(response); // Will throw if there's an error
                return response.Data;
            }
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
