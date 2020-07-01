using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TenmoServer.DAO;

namespace TenmoServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class TransferController : ControllerBase
    {
        private IAccountDAO accountDAO;
        protected string UserName
        {
            get
            {
                return User?.Identity?.Name;
            }
        }
        public TransferController(IAccountDAO accountDAO)
        {
            this.accountDAO = accountDAO;
        }
        [HttpGet]
        public IActionResult GetAccounts()
        {
            return Ok(accountDAO.GetAccount());
        }
    }
}