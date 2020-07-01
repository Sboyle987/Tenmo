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

                    SqlCommand cmd = new SqlCommand("select * from accounts a join users u on a.user_id = u.user_id where username = @userName", conn);
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

        private Account ConvertReaderToAccount(SqlDataReader reader)
        {
            Account account = new Account();

            account.Account_Id = Convert.ToInt32(reader["account_id"]);
            account.User_Id = Convert.ToInt32(reader["user_id"]);
            account.Balance = Convert.ToDecimal(reader["balance"]);
            
            return account;
        }
    }
}
