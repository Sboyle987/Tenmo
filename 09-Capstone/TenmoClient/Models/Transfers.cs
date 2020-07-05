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
            Account account = new Account();
            return
                    "------------------------------------------------------\n" +
                    $"Id: {Transfer_Id,15}\n" +
//                  $"User Name {account.Username}\n" +   // TODO Figure out how to get username info
                    $"From: {Account_From,13}\n" +
                    $"To: {Account_To,15}\n" +
                    $"Type: {Transfer_Type_Id,13}\n" +
                    $"Status: {null,10}Approved\n" +
                    $"Amount: {Amount:C}{null, 13}\n" +
                     "------------------------------------------------------\n";
        }
    }
}
