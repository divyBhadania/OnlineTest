using OnlineTest.Model;
using OnlineTest.Model.Interface;
using OnlineTest.Service.DTO;
using OnlineTest.Service.Interface;

namespace OnlineTest.Service.Services
{
    public class TechnologyService : ITechnologyService
    {
        private readonly ITechnologyRepository _TechnologyRepository;
        public TechnologyService(ITechnologyRepository TechnologyRepository)
        {
            _TechnologyRepository = TechnologyRepository;
        }
        public List<TechnologyDTO> GetAll()
        {
            var data = new List<TechnologyDTO>();
            _TechnologyRepository.GetAll().ToList().ForEach(item => data.Add(new TechnologyDTO()
            {
                TechName = item.TechName.ToUpper(),
                CreatedBy = item.CreatedBy,
                ModifiedBy = item.ModifiedBy,
            }));
            return data;
        }
        public bool Add(TechnologyDTO technology)
        {
            var now = DateTime.Now;
            return _TechnologyRepository.Add(new Technology()
            {
                TechName = technology.TechName,
                CreatedBy = technology.CreatedBy,
                CreatedOn = now
            });
        }

        public TechnologyDTO GetByName(string TechName)
        {
            var data = _TechnologyRepository.GetByName(TechName);
            if (data != null)
                return new TechnologyDTO()
                {
                    TechName = data.TechName.ToUpper(),
                    CreatedBy = data.CreatedBy,
                    ModifiedBy = data.ModifiedBy,
                };
            else
                return null;
        }

        public bool Remove(TechnologyDTO technology)
        {
            throw new NotImplementedException();
        }
    }
}
