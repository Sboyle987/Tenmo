using System.Collections.Generic;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface ITransferDAO
    {
        List<Transfer> GetTransfers(Account account);
        Transfer GetTransfersById(int transfer_Id);
        Transfer TransferMoney(Transfer transfers);
    }
}