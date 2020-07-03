using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace TenmoClient.Models
{
    public class Account
    {
        public int Account_Id { get; set; }
        public int User_Id { get; set; }
        public decimal Balance { get; set; }
        public string Username { get; set; }

        // TODO 2: Only username works, other fields are not being passed into here
        public override string ToString()
        {
            return $"User ID: {Account_Id} Username: {Username}";
        }

    }
}
