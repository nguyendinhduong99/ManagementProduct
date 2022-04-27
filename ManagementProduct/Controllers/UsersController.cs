using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PM.Data.Models;
using PM.Repository.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagementAPI.Controllers
{
    [Authorize]
    [Route("api/v{version:apiVersion}/users/")]
    [ApiController]

    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _user;
        public UsersController(IUserRepository user)
        {
            _user = user;
        }
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticationModel user)
        {
            var acc = _user.Authenticate(user.Username, user.Password);
            if (acc == null)
            {
                return BadRequest(new { message = "Username or Password is incorrect !" });
            }
            return Ok(acc);
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] AuthenticationModel user)
        {
            var accUnique = _user.IsUniqueUser(user.Username);
            if (!accUnique)
            {
                return BadRequest(new { message = "Username  already exist !" });
            }

            var account = _user.Register(user.Username, user.Password);
            if (account == null)
            {
                return BadRequest(new { message = "Error while registering" });
            }
            return Ok();
        }
    }
}
