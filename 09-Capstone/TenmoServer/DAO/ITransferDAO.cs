using System.Collections.Generic;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface ITransferDAO
    {
        List<Transfer> GetTransfers(Transfer transfer);
        Transfer TransferMoney(Transfer transfers);
    }
}