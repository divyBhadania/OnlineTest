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

        [HttpGet]
        public ActionResult GetAll(int page, int? limit = null)
        {
            try
            {
                page = page == null ? 1 : page;
                var test = _testService.GetAllTest(page, limit);
                return Ok(JsonConvert.SerializeObject(new
                {
                    data = test,
                    status = 200,
                    message = "Test data"
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

        [HttpGet]
        public ActionResult Get(int id)
        {
            try
            {
                var test = _testService.GetTesyById(id);
                return Ok(JsonConvert.SerializeObject(new
                {
                    data = test,
                    status = 200,
                    message = "Test data"
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
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult AddTest(AddTestDTO addTestDTO)
        {
            try
            {
                addTestDTO.CreatedBy = Convert.ToInt16(User.FindFirst("UserId").Value);
                if (_testService.AddTest(addTestDTO))
                    return Ok(JsonConvert.SerializeObject(new
                    {
                        data = "",
                        status = 200,
                        message = "Test added successfully"
                    }));
                else
                    return BadRequest(JsonConvert.SerializeObject(new
                    {
                        data = "",
                        status = 400,
                        message = "Test added successfully"
                    })) ;
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

        [HttpPut]
        public ActionResult updateTest(TestDTO testDTO)
        {
            _testService.UpdateTest(testDTO);
            return Ok();
        }
    }
}
