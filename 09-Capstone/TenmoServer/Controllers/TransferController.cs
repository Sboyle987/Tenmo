using System;
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
        private IUserDAO userDAO;
        
        public TransferController(IAccountDAO accountDAO, ITransferDAO transferDAO, IUserDAO userDAO)
        {
            this.accountDAO = accountDAO;
            this.transferDAO = transferDAO;
            this.userDAO = userDAO;
        }
        [HttpGet]
        public IActionResult GetAccounts()
        {
            return Ok(accountDAO.GetAccounts());
        }
        [HttpPost("exchange")]
        public IActionResult TransferMoney(Transfer transfer)
        {

            transfer.Account_From = UserId;            
            transfer.Transfer_Type_Id = 2;
            transfer.Transfer_Status_Id = 2;
            transferDAO.TransferMoney(transfer);
            return Ok();
        }
    }
}