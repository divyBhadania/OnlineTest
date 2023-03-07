using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineTest.Model;
using OnlineTest.Service.DTO;
using OnlineTest.Service.Interface;
using System.ComponentModel.DataAnnotations;

namespace OnlineTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize]
        
        public ActionResult Get(int? limit = null , int? page=null)
        {
            try
            {
                page = page == null ? 1 : page;
                var user = _userService.GetUsers((int)page, limit);
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
                }), new JsonSerializerSettings { Formatting = Formatting.Indented }));
            }
        }

        [HttpPost("add")]
        public IActionResult Post(UserDTO user)
        {
            try
            {
                var data = _userService.SeachUser(email : user.Email);
                if(data.Count != 0)
                {
                    return BadRequest(JsonConvert.SerializeObject(new
                    {
                        data = "",
                        status = 400,
                        message = "Email id already exits."
                    }));
                }
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
        public IActionResult UpdateUser(UserDTO user)
        {
            try
            {
                var data = _userService.SeachUser(email: user.Email);
                if (data.Count != 0 && data.FirstOrDefault().Id != user.Id)
                {
                    return BadRequest(JsonConvert.SerializeObject(new
                    {
                        data = "",
                        status = 400,
                        message = "Email id already exits with different account."
                    }));
                }
                if (_userService.SeachUser(user.Id).Count == 0)
                    return BadRequest(JsonConvert.SerializeObject(new
                    {
                        data = "",
                        status = 400,
                        message = "Invalid User Id."
                    }));
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
                        message = "User data Not updated"
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
                var userDto = _userService.GetUsers(next : 0).Where(i => i.Id == id).FirstOrDefault();
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
                if (_userService.ActiveUser(id, isactive))
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

        [HttpPut("changepassword")]
        public ActionResult ChangePassword([Required] int id, [Required] string oldpassword, [Required] string password)
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