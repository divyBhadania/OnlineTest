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
        public TechnologyController(ITechnologyService technologyService)
        {
            _TechnologyService = technologyService;
        }
        [HttpGet]
        public ActionResult<List<TechnologyDTO>> GetTech()
        {
            try
            {
                var item = _TechnologyService.GetAll().Select(x => x.TechName);
                if (item != null)
                    return Ok(JsonConvert.SerializeObject(new
                    {
                        data = item,
                        status = 200,
                        message = "All technology data"
                    }));
                else
                    return NotFound(JsonConvert.SerializeObject(new
                    {
                        data = "",
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
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult Post(string name)
        {
            try
            {
                var technologyDTO = new TechnologyDTO();
                technologyDTO.TechName = name.ToUpper();
                technologyDTO.CreatedBy = Convert.ToInt16(User.FindFirst("UserId").Value);
                if (_TechnologyService.GetByName(technologyDTO.TechName) == null)
                {
                    if (_TechnologyService.Add(technologyDTO))
                        return Ok(JsonConvert.SerializeObject(new
                        {
                            data = "",
                            status = 200,
                            message = "Technology added successfully"
                        }));
                    else
                        return BadRequest(JsonConvert.SerializeObject(new
                        {
                            data = "",
                            status = 400,
                            message = "Technology Not added"
                        }));
                }
                else
                    return BadRequest(JsonConvert.SerializeObject(new
                    {
                        data = "",
                        status = 400,
                        message = "Technology already exits."
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
