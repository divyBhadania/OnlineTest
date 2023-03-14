using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineTest.Service.DTO;
using OnlineTest.Service.Interface;

namespace OnlineTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        public readonly IUserServices _userService;
        public UserController(IUserServices userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult Get(int? limit = null , int? page=null)
        {
            return Ok(_userService.GetUsers(page == null ? 1 : (int)page , limit));
        }

        [HttpPost("add")]
        public ActionResult Post(AddUserDTO user)
        {
            return Ok(_userService.AddUser(user));
        }

        [HttpPut("update")]
        public ActionResult UpdateUser(AddUserDTO user)
        {
            return Ok(_userService.UpdateUser(user));
        }

        [HttpDelete("delete")]
        public ActionResult Delete(int id)
        {
            return Ok(_userService.DeleteUser(id));
        }
    }
}