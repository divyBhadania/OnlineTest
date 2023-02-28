using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using OnlineTest.Model;
using OnlineTest.Service.Interface;
using OnlineTest.Service.Services;

namespace OnlineTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class userController : ControllerBase
    {
        private readonly IUserService _UserService;
        public userController(IUserService UserService) {
            _UserService = UserService;
        }

        [HttpGet]
        public ActionResult<User> Get()
        {
            var data = _UserService.GetAllUser();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetById(int id)
        {
            var data = _UserService.GetUserById(id);
            return data != null ? Ok(data) : NotFound("User Not Found");
        }

        [HttpPost]
        [Route("adduser")]
        public  ActionResult<User> Post(User user)
        {
            _UserService.AddUser(user);
            return Ok();
        }

        [HttpDelete()]
        [Route("deleteuser")]
        public ActionResult<User> Delete(int id)
        {
            var user = _UserService.GetUserById(id);
            if(user != null)
            {
                _UserService.DeleteUserById(user);
            }
            return user == null ? NotFound() : Ok();
        }

        [HttpPut()]
        [Route("updateuser")]
        public ActionResult<User> Put(int id, User user)
        {
            if (id != user.UserId)
                return BadRequest();
            _UserService.UpdateUser(user);
            return Ok();
        }

    }
}