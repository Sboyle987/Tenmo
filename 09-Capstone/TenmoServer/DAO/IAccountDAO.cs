using System.Collections.Generic;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface IAccountDAO
    {
        Account GetAccountByName(string userName);
        decimal GetBalanceByName(string userName);

        Dictionary<Account, LoginUser> GetAccount();

    }
}