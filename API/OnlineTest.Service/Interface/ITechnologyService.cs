using OnlineTest.Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTest.Service.Interface
{
    public interface ITechnologyService
    {
        List<TechnologyDTO> GetAll();
        TechnologyDTO GetByName(string TechName);
        bool Add(TechnologyDTO technology);
        bool Remove(TechnologyDTO technology);
    }
}
