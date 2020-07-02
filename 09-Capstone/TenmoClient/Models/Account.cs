using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TenmoServer.Models
{
    public class Account
    {
        public int Account_Id { get; set; }
        public int User_Id { get; set; }
        public decimal Balance { get; set; }
    

    public override string ToString()
    {
            return $"Account Id: {Account_Id} User Id: {User_Id}, Balance: {Balance}";
    }

    }
}
