using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using OnlineTest.Model;

namespace OnlineTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class userController : ControllerBase
    {
        private readonly OnlineTestContext _context;
        public userController(OnlineTestContext context) {
            _context= context;
        }

        [HttpGet]
        public async Task<ActionResult<User>> Get()
        {
            var data = await _context.Users.ToListAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetById(int id)
        {
            var data = await _context.Users.FindAsync(id);
            return data != null ? Ok(data) : NotFound("User Not Found");
        }

        [HttpPost]
        [Route("adduser")]
        public async Task<ActionResult<User>> Post(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete()]
        [Route("deleteuser")]
        public async Task<ActionResult<User>> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return user == null ? NotFound() : Ok();
        }

        [HttpPut()]
        [Route("updateuser")]
        public async Task<ActionResult<User>> Put(int id , User user)
        {
            if (id != user.UserId)
                return BadRequest();

            return Ok();
        }
      
    }
}