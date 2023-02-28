using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using OnlineTest.Model;

namespace OnlineTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserDbContext _context;
        public UserController(UserDbContext context) {
            _context= context;
        }

        [HttpGet]
        public async Task<ActionResult<User>> Get()
        {
            var data = await _context.User.ToListAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetById(int id)
        {
            var data = await _context.User.FindAsync(id);
            return data != null ? Ok(data) : NotFound("User Not Found");
        }

        [HttpPost]
        public async Task<ActionResult<User>> Post(int id, User user)
        {
            if (id != user.UserId)
                return BadRequest();
            _context.User.Add(user);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> Delete(int id)
        {
            var user = await _context.User.FindAsync(id);
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            return user == null ? NotFound() : Ok();
        }

      
    }
}