using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface IAccountDAO
    {
        Account GetAccountByName(string userName);
        decimal GetBalanceByName(string userName);

    }
}