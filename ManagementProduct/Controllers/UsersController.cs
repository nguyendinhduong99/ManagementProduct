using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PM.Data.Models;
using PM.Data.Models.Dtos;
using PM.Repository.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagementAPI.Controllers
{
    //[Authorize]
    [Route("api/v{version:apiVersion}/users/")]
    [ApiController]

    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _user;
        private readonly IMapper _mapper;
        public UsersController(IUserRepository user, IMapper mapper)
        {
            _user = user;
            _mapper = mapper;
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

        [HttpPatch("update-pass/{id}")]
        public IActionResult UpdatePass(string userName, [FromBody] UpdateUserDto userDto)
        {
            if (userDto == null || userDto.UserName != userName)
            {
                return BadRequest(ModelState);
            }

            var ptObj = _mapper.Map<User>(userDto);
            if (!_user.UpdatePass(ptObj) || ptObj.Password != ptObj.ConfirmPassword)
            {
                ModelState.AddModelError("", $"Something went  wrorng when update the record {ptObj.FullName}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpGet]
        public IActionResult GetUsers(string sortBy, string searchString, int? pageNumber)
        {
           
            var objList = _user.GetUsers( sortBy,  searchString, pageNumber);

            var objListDto = new List<UserDto>();

            foreach (var item in objList)
            {
                objListDto.Add(_mapper.Map<UserDto>(item));
            }

            return Ok(objListDto);
        }
        [HttpGet("get-user-id/{id}", Name = "GetUserById")]
        public IActionResult GetUserById(int id)
        {
            var obj = _user.GetUserById(id);
            if (obj == null)
            {
                return NotFound();
            }
            else
            {
                var objDto = _mapper.Map<UserDto>(obj);
                return Ok(objDto);
            }
        }
        [HttpPut("{id}", Name = "UpdateUser")]
        public IActionResult UpdateUser(int id, [FromBody] UserDto userDto)
        {
            if (userDto == null || userDto.Id != id)
            {
                return BadRequest(ModelState);
            }

            var userObj = _mapper.Map<User>(userDto);
            if (!_user.UpdateUser(userObj))
            {
                ModelState.AddModelError("", $"Something went  wrorng when update the record {userObj.FullName}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteUser")]
        public IActionResult DeleteUser(int id)
        {
            if (!_user.UserExist(id))
            {
                return NotFound();
            }


            var userObj = _user.GetUserById(id);
            if (!_user.DeleteUser(userObj))
            {
                ModelState.AddModelError("", $"Something went  wrorng when delete the record {userObj.FullName}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
