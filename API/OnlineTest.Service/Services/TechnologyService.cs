using AutoMapper;
using OnlineTest.Model;
using OnlineTest.Model.Interface;
using OnlineTest.Service.DTO;
using OnlineTest.Service.Interface;

namespace OnlineTest.Service.Services
{
    public class TechnologyService : ITechnologyService
    {
        private readonly ITechnologyRepository _technologyRepository;
        private readonly IMapper _mapper;
        public TechnologyService(ITechnologyRepository technologyRepository, IMapper mapper)
        {
            _technologyRepository = technologyRepository;
            _mapper = mapper;
        }
        public ResponseDTO GetAll()
        {
            try
            {
                var Tech = _mapper.Map<List<TechnologyDTO>>(_technologyRepository.GetAll().ToList())
                    .Select(i => new
                    {
                        id = i.Id,
                        techName = i.TechName
                    }); ;
                return new ResponseDTO
                {
                    Data = Tech,
                    Status = 200,
                    Message = "Sucess"
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    Status = 400,
                    Message = "Failed",
                    Error = ex.Message
                };
            }
        }
        public ResponseDTO Add(string name, int id)
        {
            try
            {
                if (_technologyRepository.GetByName(name.ToUpper()) != null)
                    return new ResponseDTO
                    {
                        Status = 400,
                        Message = "Technology already exits.",
                    };
                var data = new Technology
                {
                    TechName = name.ToUpper(),
                    CreatedBy = id,
                    CreatedOn = DateTime.UtcNow
                };
                if (_technologyRepository.Add(data).Result)
                    return new ResponseDTO
                    {
                        Status = 200,
                        Message = "Technology added successfully",
                    };
                else
                    return new ResponseDTO
                    {
                        Status = 400,
                        Message = "Technology Not added.",
                    };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    Status = 400,
                    Message = "Failed",
                    Error = ex.Message
                };
            }

        }

        public ResponseDTO Update(int id, string oldTech, string newTech)
        {
            try
            {
                var tech = _technologyRepository.GetByName(oldTech.ToUpper());
                if (tech == null)
                    return new ResponseDTO
                    {
                        Status = 404,
                        Message = "Technology not exits.",
                    };
                else
                {
                    tech.TechName = newTech.ToUpper();
                    tech.ModifiedOn = DateTime.UtcNow;
                    tech.ModifiedBy = id;
                    if (_technologyRepository.Update(tech).Result)
                        return new ResponseDTO
                        {
                            Status = 200,
                            Message = "Technology updated successfully",
                        };
                    else
                        return new ResponseDTO
                        {
                            Status = 400,
                            Message = "Technology not update successfully",
                        };
                }
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    Status = 400,
                    Message = "Failed",
                    Error = ex.Message
                };
            }
        }
        public bool Remove(TechnologyDTO technology)
        {
            throw new NotImplementedException();
        }
    }
}
