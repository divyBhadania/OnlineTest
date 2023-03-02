using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using OnlineTest.Model;
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
        public ActionResult Get()
        {
            try
            {
                var user = _userService.GetUsers();
                if (user.Count > 0)
                    return Ok(JsonConvert.SerializeObject(new
                    {
                        data = user,
                        status = 200,
                        message = "All users data"
                    }));
                else
                    return NotFound(JsonConvert.SerializeObject(new
                    {
                        data = user,
                        status = 404,
                        message = "No data Found"
                    }));
            }
            catch (Exception ex)
            {
                return BadRequest(JsonConvert.SerializeObject(new
                {
                    data = "",
                    status = 400,
                    message = ex.Message
                }));
            }
        }

        [HttpPost("add")]
        public IActionResult Post(UserDTO user)
        {
            try
            {
                if (_userService.AddUser(user))
                    return Ok(JsonConvert.SerializeObject(new
                    {
                        data = "",
                        status = 200,
                        message = "User added successfully"
                    }));
                else
                    return BadRequest(JsonConvert.SerializeObject(new
                    {
                        data = "",
                        status = 400,
                        message = "User Not added"
                    }));
            }
            catch (Exception ex)
            {
                return BadRequest(JsonConvert.SerializeObject(new
                {
                    data = "",
                    status = 400,
                    message = ex.Message
                }));
            }
        }

        [HttpPut("update")]
        public IActionResult UpdateUser(int id, UserDTO user)
        {
            if (user.Id != id)
                return BadRequest(JsonConvert.SerializeObject(new
                {
                    data = "",
                    status = 400,
                    message = "Data does not match User Id."
                }));
            try
            {
                if (_userService.UpdateUser(user))
                    return Ok(JsonConvert.SerializeObject(new
                    {
                        data = "",
                        status = 200,
                        message = "User update successfully"
                    }));
                else
                    return BadRequest(JsonConvert.SerializeObject(new
                    {
                        data = "",
                        status = 400,
                        message = "User Not updated"
                    }));
            }
            catch (Exception ex)
            {
                return BadRequest(JsonConvert.SerializeObject(new
                {
                    data = "",
                    status = 400,
                    message = ex.Message
                }));
            }
        }

        [HttpDelete("delete")]
        public IActionResult Delete(int id)
        {
            try
            {
                var userDto = _userService.GetUsers().Where(i => i.Id == id).FirstOrDefault();
                if (userDto == null)
                {
                    return NotFound(JsonConvert.SerializeObject(new
                    {
                        data = "",
                        status = 404,
                        message = "No User Found"
                    }));
                }
                else if (_userService.DeleteUser(userDto))
                    return Ok(JsonConvert.SerializeObject(new
                    {
                        data = "",
                        status = 200,
                        message = "User deleted successfully"
                    }));
                else
                    return BadRequest(JsonConvert.SerializeObject(new
                    {
                        data = "",
                        status = 400,
                        message = "User Not deleted"
                    }));
            }
            catch (Exception ex)
            {
                return BadRequest(JsonConvert.SerializeObject(new
                {
                    data = "",
                    status = 400,
                    message = ex.Message
                }));
            }
        }

        [HttpGet("search")]
        public ActionResult<UserDTO> SearchQuery(int? id = null, string? name = null, string? email = null, string? mobile = null, bool? isactive = null)
        {
            try
            {
                var users = _userService.SeachUser(id, name, email, mobile, isactive);
                if (users != null)
                    return Ok(JsonConvert.SerializeObject(new
                    {
                        data = users,
                        status = 200,
                        message = "All users list related to query."
                    }));
                else
                    return NotFound(JsonConvert.SerializeObject(new
                    {
                        data = users,
                        status = 404,
                        message = "No data Found."
                    }));
            }
            catch (Exception ex)
            {
                return BadRequest(JsonConvert.SerializeObject(new
                {
                    data = "",
                    status = 400,
                    message = ex.Message
                }));
            }
        }

        [HttpPut("active")]
        public ActionResult ActiveUser(int id, bool isactive = true)
        {
            try
            {
                if(_userService.ActiveUser(id, isactive))
                {
                    return Ok(JsonConvert.SerializeObject(new
                    {
                        data = "",
                        status = 200,
                        message = "Change user status successfully."
                    }));
                }
                else
                    return BadRequest(JsonConvert.SerializeObject(new
                    {
                        data = "",
                        status = 400,
                        message = "No change in user status."
                    }));
            }
            catch(Exception ex)
            {
                return BadRequest(JsonConvert.SerializeObject(new
                {
                    data = "",
                    status = 400,
                    message = ex.Message
                }));
            }
        }

        [HttpPut("changepassword")]
        public ActionResult ChangePassword([Required] int id, [Required] string oldpassword , [Required]string password)
        {
            try
            {
                if (_userService.ChangePassword(id, oldpassword, password))
                {
                    return Ok(JsonConvert.SerializeObject(new
                    {
                        data = "",
                        status = 200,
                        message = "Password change successfully."
                    }));
                }
                else
                    return NotFound(JsonConvert.SerializeObject(new
                    {
                        data = "",
                        status = 404,
                        message = "Incorect old password."
                    }));
            }
            catch (Exception ex)
            {
                return BadRequest(JsonConvert.SerializeObject(new
                {
                    data = "",
                    status = 400,
                    message = ex.Message
                }));
            }
        }
    }
}