﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        public Transfer TransferMoney(Transfer transfer)
        {
            string sql = @"
                Begin Transaction
                Update accounts set balance = balance - @amount WHERE account_id = @accountIdFrom;
                Update accounts set balance = balance + @amount WHERE account_id = @accountIdTo;
                Insert INTO transfers (transfer_type_id, transfer_status_id, account_from, account_to, amount)
                    values(@transferType, @transferStatus, @accountIdFrom, @accountIdTo, @amount) SELECT @@IDENTITY
                Commit Transaction
                ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@amount", transfer.Amount);
                    cmd.Parameters.AddWithValue("@accountIdFrom", transfer.AccountFrom);
                    cmd.Parameters.AddWithValue("@accountIdTo", transfer.AccountTo);
                    cmd.Parameters.AddWithValue("@transferType", transfer.TransferTypeId);
                    cmd.Parameters.AddWithValue("@transferStatus", transfer.TransferStatusId);
                    transfer.TransferId = Convert.ToInt32(cmd.ExecuteScalar());
                    return transfer;
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }
        public List<Transfer> GetTransfers(string userName)
        {
            Transfer transfer;
            List<Transfer> transfers = new List<Transfer>();
            string sql = @"
                Select uTO.username user_to, t.*, uFROM.username user_from
                FROM transfers t
                Join accounts aTO ON aTO.account_id = t.account_to
                Join users uTO ON uTO.user_id = aTO.user_id
                Join accounts aFROM ON aFROM.account_id = t.account_from
                Join users uFROM ON uFROM.user_id = aFROM.user_id
                WHERE uTO.username = @userName OR uFrom.username = @userName
                ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@userName", userName);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        transfer = ConvertReaderToTransfer(reader);
                        transfers.Add(transfer);
                    }

                }
            }
            catch (SqlException)
            {
                throw;
            }
            return transfers;
        }
        public Transfer GetTransfersById(int transfer_Id)
        {
            Transfer transfer = new Transfer();           
            string sql = @"
                            Select uTO.username user_to, t.*, uFROM.username user_from
                            FROM transfers t
                            Join accounts aTO ON aTO.account_id = t.account_to
                            Join users uTO ON uTO.user_id = aTO.user_id
                            Join accounts aFROM ON aFROM.account_id = t.account_from
                            Join users uFROM ON uFROM.user_id = aFROM.user_id
                            WHERE transfer_id = @transfer_Id
                          ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@transfer_Id", transfer_Id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        transfer = ConvertReaderToTransfer(reader);
                    }

                }
            }
            catch (SqlException)
            {
                throw;
            }
            return transfer;
        }
        private Transfer ConvertReaderToTransfer(SqlDataReader reader)
        {
            Transfer transfer = new Transfer();

            transfer.AccountFrom = Convert.ToInt32(reader["account_from"]);
            transfer.TransferId = Convert.ToInt32(reader["transfer_id"]);
            transfer.TransferTypeId = Convert.ToInt32(reader["transfer_type_id"]);
            transfer.TransferStatusId = Convert.ToInt32(reader["transfer_status_id"]);
            transfer.AccountTo = Convert.ToInt32(reader["account_to"]);
            transfer.Amount = Convert.ToDecimal(reader["amount"]);
            transfer.UsernameFrom = Convert.ToString(reader["user_from"]);
            transfer.UsernameTo = Convert.ToString(reader["user_to"]);


            return transfer;
        }
    }
}
