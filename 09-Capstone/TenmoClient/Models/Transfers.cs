using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TenmoClient.Models
{
    public class Transfer
    {
        public int Transfer_Id { get; set; }
        public int Transfer_Type_Id { get; set; } = 2;
        public int Transfer_Status_Id { get; set; } = 2;
        public int Account_From { get; set; }
        public int Account_To { get; set; }
        public decimal Amount { get; set; }
        public override string ToString()
        {
            return $"Id: {Transfer_Id}\n" +
                    $"From: {Account_From}\n" +
                    $"To: {Account_To}\n" +
                    $"Type: {Transfer_Type_Id}\n" +
                    $"Status: Approved\n" +
                    $"Amount: {Amount}\n";
                    
        }
    }
}
