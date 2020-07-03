﻿using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Net;
using System.Text;
using TenmoServer.Models;

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
            RestRequest request = new RestRequest("transfer");
            IRestResponse<List<Account>> response = client.Get<List<Account>>(request);

            CheckError(response);

            List<Account> accounts = response.Data;
            
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
