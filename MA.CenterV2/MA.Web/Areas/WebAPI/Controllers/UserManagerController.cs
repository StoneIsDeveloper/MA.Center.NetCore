using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MA.DBAccess;
using MA.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MA.Web.Areas.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagerController : ControllerBase
    {
        public readonly UserService userService = new UserService();

        [HttpGet]
        public ActionResult<IEnumerable<UserInfo>> GetUsers()
        {
            var users = userService.GetUseInfos();
            return users;
        }
    }
}