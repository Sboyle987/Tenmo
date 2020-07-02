using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public class TransferSqlDAO : ITransferDAO
    {
        private readonly string connectionString;

        public TransferSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }
        public void TransferMoney(Transfer transfer)
        {
            string sql = @"
                Begin Transaction
                Update accounts set balance = balance - @amount WHERE account_id = @accountIdFrom;
                Update accounts set balance = balance + @amount WHERE account_id = @accountIdTo;
                Insert INTO transfers (transfer_type_id, transfer_status_id, account_from, account_to, amount)
                    values(@transferType, @transferStatus, @accountIdFrom, @accountIdTo, @amount);
                Commit Transaction
                ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@amount", transfer.Amount);
                    cmd.Parameters.AddWithValue("@accountIdFrom", transfer.Account_From);
                    cmd.Parameters.AddWithValue("@accountIdTo", transfer.Account_To);
                    cmd.Parameters.AddWithValue("@transferType", transfer.Transfer_Type_Id);
                    cmd.Parameters.AddWithValue("@transferStatus", transfer.Transfer_Status_Id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }
    }
}
