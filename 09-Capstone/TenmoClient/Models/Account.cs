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
        public string Username { get; set; }

        // TODO 2: ints are represented as 0? but balance and Username both show up fine?
        public override string ToString()
    {
            return $"Username: {Username}";
    }

    }
}
