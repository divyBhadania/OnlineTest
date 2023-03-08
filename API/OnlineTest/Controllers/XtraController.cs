using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineTest.Service.DTO;
using OnlineTest.Service.Interface;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace OnlineTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class XtraController : ControllerBase
    {
        public readonly IUserServices _userService;
        public XtraController(IUserServices userService)
        {
            _userService = userService;
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
                if (RequestUserId() != id || RequestUserRoles() == "Admin")
                {
                    return BadRequest(JsonConvert.SerializeObject(new
                    {
                        data = "",
                        status = 200,
                        message = "Your are not allowed to change someone else password."
                    }));
                }
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

        #region Extra methods
        private string RequestUserRoles()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            return identity.Claims.FirstOrDefault(i => i.Type == "Roles")?.Value ?? "";
        }

        private int RequestUserId()
        {
            return (int)Convert.ToInt64((HttpContext.User.Identity as ClaimsIdentity).Claims.FirstOrDefault(i => i.Type == "UserId")?.Value);
        }

        #endregion
    }
}
