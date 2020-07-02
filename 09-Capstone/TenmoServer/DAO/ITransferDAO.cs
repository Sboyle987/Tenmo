using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface ITransferDAO
    {
        void TransferMoney(Transfers transfers);
    }
}