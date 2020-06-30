using System;
using System.Data.SqlClient;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public class AccountSqlDAO : IAccountDAO
    {
        private readonly string connectionString;
        private readonly User user;

        public decimal GetBalance()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("Select balance From accounts Where user_id = @user_id", conn);
                    cmd.Parameters.AddWithValue("@user_id", user.UserId);
                    decimal balance = Convert.ToDecimal(cmd.ExecuteScalar());
                    return balance;
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }
    }
}
