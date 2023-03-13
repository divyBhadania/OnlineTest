using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineTest.Service.DTO;
using OnlineTest.Service.Interface;
using System.Collections.Generic;

namespace OnlineTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TestController : ControllerBase
    {
        private readonly ITestService _testService;
        public TestController(ITestService testService)
        {
            _testService = testService;
        }

        [HttpGet("all")]
        public ActionResult GetAll(int page, int? limit = null)
        {
            return Ok(_testService.GetAllTest(page == null ? 1 : page, limit));
        }

        [HttpGet]
        public ActionResult Get(int id)
        {
            return Ok(_testService.GetTesyById(id));
        }

        [HttpPost("add")]
        //[Authorize(Roles = "Admin,Moderator")]
        public ActionResult AddTest(AddTestDTO addTestDTO)
        {
            return Ok(_testService.AddTest(Convert.ToInt32(User.FindFirst("UserId").Value), addTestDTO));
        }

        [HttpPut("update")]
        public ActionResult updateTest(UpdateTestDTO testDTO)
        {
            return Ok(_testService.UpdateTest(testDTO));
        }
    }
}
