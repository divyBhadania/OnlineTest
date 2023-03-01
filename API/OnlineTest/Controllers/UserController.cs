using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OnlineTest.Service.DTO;
using OnlineTest.Service.Interface;
using System.ComponentModel.DataAnnotations;

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

        [HttpPost("add")]
        public IActionResult Post(UserDTO user)
        {
            return Ok(_userService.AddUser(user));
        }

        [HttpPut("update")]
        public IActionResult UpdateUser(UserDTO user)
        {
            try
            {
                _userService.UpdateUser(user);
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpDelete("delete")]
        public IActionResult Delete(int id)
        {
            try
            {
                var userDto = _userService.GetUsers().Where(i => i.UserId==id).FirstOrDefault();
                _userService.DeleteUser(userDto);
                return Ok();   
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("search")]
        public ActionResult<UserDTO> SearchQuery(int id, int mobile)
        {
            return Ok();
        }
    }
}