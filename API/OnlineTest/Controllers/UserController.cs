using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using OnlineTest.Model;
using OnlineTest.Service.DTO;
using OnlineTest.Service.Interface;
using OnlineTest.Service.Services;

namespace OnlineTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class userController : ControllerBase
    {
        public readonly IUserService _userService;
        public userController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<UserDTO> Get()
        {
            var data = _userService.GetUsers();
            return Ok(data);
        }

        [HttpPost("adduser")]
        public IActionResult Post(UserDTO user)
        {
            return Ok(_userService.AddUser(user));
        }

        [HttpPut("updateuser")]
        public IActionResult UpdateUser(UserDTO user)
        {
            try
            {
                _userService.UpdateUser(user);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}