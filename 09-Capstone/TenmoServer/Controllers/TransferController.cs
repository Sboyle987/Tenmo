﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TenmoServer.DAO;
using TenmoServer.Models;

namespace TenmoServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class TransferController : TenmoController
    {
        private IAccountDAO accountDAO;
        private ITransferDAO transferDAO;
        private IUserDAO userDAO;

        public TransferController(IAccountDAO accountDAO, ITransferDAO transferDAO, IUserDAO userDAO)
        {
            this.accountDAO = accountDAO;
            this.transferDAO = transferDAO;
            this.userDAO = userDAO;
        }
        [HttpGet]
        public ActionResult<List<Account>> GetAccounts() //THIS IS WHAT CHANGED, the action result had no <List<Account>>
        {
            List<Account> accounts = new List<Account>();
            accounts = accountDAO.GetAccounts();
            return Ok(accounts);
        }
        [HttpPost("exchange")]
        public IActionResult TransferMoney(Transfer transfer)
        {
            IActionResult result;
            decimal balance = accountDAO.GetBalanceByName(UserName);
            if (transfer.Amount <= balance)
            {
                transfer.AccountFrom = UserId;
                transfer.TransferTypeId = 2;
                transfer.TransferStatusId = 2;
                transferDAO.TransferMoney(transfer);
                result = Ok();
                return result;
            }
            else result = BadRequest();
            return result;
        }
        [HttpGet("list")]
        public IActionResult GetTransfers()
        {
            return Ok(transferDAO.GetTransfers(UserName));
        }
        [HttpGet("{id}")]
        public IActionResult GetTransfersById(int id)
        {
            return Ok(transferDAO.GetTransfersById(id));
        }
    }
}