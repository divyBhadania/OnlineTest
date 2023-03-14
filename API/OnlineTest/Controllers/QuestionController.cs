using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineTest.Model.Interface;
using OnlineTest.Service.DTO;
using OnlineTest.Service.Interface;

namespace OnlineTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;
        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }
        [HttpGet("test")]
        public ActionResult ByTestId(int testId)
        {
            return Ok(_questionService.GetQuesByTestId(testId));
        }
        [HttpGet]
        public ActionResult ById(int id)
        {
            return Ok(_questionService.GetById(id));
        }
        [HttpPost("add")]
        public ActionResult add(QuestionDTO questionDTO)
        {
            return Ok(_questionService.AddQuestion(questionDTO));
        }
        [HttpPut]
        public ActionResult Update(QuestionDTO questionDTO)
        {
            return Ok(_questionService.UpdateQuestion(questionDTO));
        }
    }
}
