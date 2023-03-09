using OnlineTest.Model;
using OnlineTest.Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTest.Service.Interface
{
    public interface IUserRolesService
    {
        List<int> GetById(int id);
        bool AddRole(UserRolesDTO userRole);
        bool RemoveRole(UserRolesDTO userRole);
    }
}
