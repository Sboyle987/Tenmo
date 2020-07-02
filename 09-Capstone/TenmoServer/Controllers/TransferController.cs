﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private Transfers transfer;
       
        public TransferController(IAccountDAO accountDAO)
        {
            this.accountDAO = accountDAO;
        }
        [HttpGet]
        public IActionResult GetAccounts()
        {
            return Ok(accountDAO.GetAccounts());
        }
        [HttpPost("exchange")]
        public IActionResult TransferMoney()
        {
            transferDAO.TransferMoney(transfer);
            return Ok();
        }
    }
}