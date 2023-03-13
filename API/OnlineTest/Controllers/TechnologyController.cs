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
    public class TechnologyController : ControllerBase
    {
        private readonly ITechnologyService _TechnologyService;
        //private readonly int UserId ;
        public TechnologyController(ITechnologyService technologyService)
        {
            _TechnologyService = technologyService;
            //UserId = Convert.ToInt32(User.FindFirst("UserId").Value);
        }
        [HttpGet]
        public ActionResult<List<TechnologyDTO>> GetTech()
        {
            return Ok(_TechnologyService.GetAll());
        }

        [HttpPost("add")]
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult Post(string name)
        {
            return Ok(_TechnologyService.Add(name, Convert.ToInt32(User.FindFirst("UserId").Value)));
        }

        [HttpPut("update")]
        public ActionResult Put(string oldTech, string newTech)
        {
            return Ok(_TechnologyService.Update(Convert.ToInt32(User.FindFirst("UserId").Value), oldTech,newTech));
        }
    }
}