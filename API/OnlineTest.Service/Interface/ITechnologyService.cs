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
        ResponseDTO GetAll();
        ResponseDTO Add(string name , int id);
        ResponseDTO Update(int id,string oldTech, string newTech);
        bool Remove(TechnologyDTO technology);
    }
}
