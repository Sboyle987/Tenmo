using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public class AccountSqlDAO : IAccountDAO
    {
        private readonly string connectionString;
        private readonly User user;

        public AccountSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }
        public decimal GetBalanceByName(string userName)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("select balance from accounts a join users u on a.user_id = u.user_id where username = @userName", conn);
                    cmd.Parameters.AddWithValue("@userName", userName);
                    decimal balance = Convert.ToDecimal(cmd.ExecuteScalar());
                    return balance;
                }
            }
            catch (SqlException)
            {
                throw;
            }

        }
        public Account GetAccountByName(string userName)
        {
            Account account = new Account();
            try
            {
                

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("select a.*, username from accounts a join users u on a.user_id = u.user_id where username = @userName", conn);
                    cmd.Parameters.AddWithValue("@userName", userName);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        account = ConvertReaderToAccount(reader);
                    }

                }
            }
            catch (SqlException)
            {
                throw;
            }
            return account;
        }
        public List<Account> GetAccount()
        {
            List<Account> accounts = new List<Account>();

            try
            {
                //TODO Make it so this list doesn't include current logged in user

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    //We only need the user Id and the Username, but this query isn't satisfying our Convert methods. 
                    //Should we just grab all the information and then deal with it by parsing it in C#?
                    //Reading from two tables in a query/reading with a join
                    SqlCommand cmd = new SqlCommand("select a.*, username from accounts a join users u on a.user_id = u.user_id", conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Account account = ConvertReaderToAccount(reader);
                        account.Balance = 0;
                        accounts.Add(account);
                    }

                }
            }
            catch (SqlException)
            {
                throw;
            }
            return accounts;
        }

        private Account ConvertReaderToAccount(SqlDataReader reader)
        {
            Account account = new Account();

            account.Account_Id = Convert.ToInt32(reader["account_id"]);
            account.User_Id = Convert.ToInt32(reader["user_id"]);
            account.Balance = Convert.ToDecimal(reader["balance"]);
            account.Username = Convert.ToString(reader["username"]);


            return account;
        }
        private LoginUser ConvertReadertoUser(SqlDataReader reader)
        {
            LoginUser user = new LoginUser();
            user.Username = Convert.ToString(reader["username"]);
            user.Password = Convert.ToString(reader["password"]);

            return user;
        }
    }
}
