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
    public class AccountController : TenmoController
    {
        private IAccountDAO accountDAO;
       
        public AccountController(IAccountDAO accountDAO)
        {
            this.accountDAO = accountDAO;
        }
        [HttpGet("balance")]
        public IActionResult GetAccountBalanceByName()
        {
            return Ok(accountDAO.GetBalanceByName(UserName));
        }
        [HttpGet]
        public IActionResult GetAccountByName()
        {
            return Ok(accountDAO.GetAccountByName(UserName));
        }
    }
}
