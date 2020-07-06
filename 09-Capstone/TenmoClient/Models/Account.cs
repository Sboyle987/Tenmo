using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace TenmoClient.Models
{
    public class Account
    {
        public int AccountId { get; set; }
        public int UserId { get; set; }
        public decimal Balance { get; set; }
        public string Username { get; set; }


        // TODO 2: Only username works, other fields are not being passed into here
        public override string ToString()
        {
            return $"User ID: {AccountId} Username: {Username}";
        }

    }
}
