using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TenmoServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TenmoController : ControllerBase
    {
        protected string UserName
        {
            get
            {
                return User?.Identity?.Name;
            }
        }
        protected int UserId
        {
            get
            {
                int userId = 0;
                Claim subjectClaim = User?.Claims?.Where(cl => cl.Type == "sub").FirstOrDefault();
                if (subjectClaim != null)
                {
                    int.TryParse(subjectClaim.Value, out userId);
                }
                return userId;
            }
        }
    }

}