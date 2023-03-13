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
        public TechnologyService(ITechnologyRepository technologyRepository , IMapper mapper)
        {
            _technologyRepository = technologyRepository;
            _mapper = mapper;
        }
        public List<TechnologyDTO> GetAll()
        {
            var data = new List<TechnologyDTO>();
            _technologyRepository.GetAll().ToList().ForEach(item => data.Add(_mapper.Map<TechnologyDTO>(item)));
            return data;
        }
        public bool Add(TechnologyDTO technology)
        {
            var data = _mapper.Map<Technology>(technology);
            data.CreatedOn = DateTime.Now;
            data.TechName = data.TechName;
            return _technologyRepository.Add(data).Result;
        }

        public TechnologyDTO GetByName(string TechName)
        {
            var data = _technologyRepository.GetByName(TechName);
            if (data != null)
                return _mapper.Map<TechnologyDTO>(data);
            else
                return null;
        }

        public bool Remove(TechnologyDTO technology)
        {
            throw new NotImplementedException();
        }
    }
}
