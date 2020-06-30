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
    public class AccountsController : ControllerBase
    {
        private readonly IAccountDAO accountDAO;

        public AccountsController(IAccountDAO _accountDAO)
        {
            accountDAO = _accountDAO;
        }
        [HttpGet]
        public IActionResult GetAccountBalance()
        {
            return Ok(accountDAO.GetBalance());
        }
    }
}
