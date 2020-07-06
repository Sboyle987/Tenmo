namespace TenmoClient.Models
{
    public class Transfer
    {
        public int TransferId { get; set; }
        public int TransferTypeId { get; set; } = 2;
        public int TransferStatusId { get; set; } = 2;
        public int AccountFrom { get; set; }
        public int AccountTo { get; set; }
        public decimal Amount { get; set; }
        public string UsernameFrom { get; set; }
        public string UsernameTo { get; set; }


        public override string ToString()
        {
            Account account = new Account();
            return
                    "------------------------------------------------------\n" +
                    $"Id: {TransferId,15}\n" +
//                  $"User Name {account.Username}\n" +   // TODO Figure out how to get username info
                    $"From: {UsernameFrom,13}\n" +
                    $"To: {UsernameTo,15}\n" +
                    $"Type: {TransferTypeId,13}\n" +
                    $"Status: {null,10}Approved\n" +
                    $"Amount: {Amount:C}{null, 13}\n" +
                     "------------------------------------------------------\n";
        }
    }
}
