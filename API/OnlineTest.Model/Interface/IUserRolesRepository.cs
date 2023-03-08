using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTest.Model.Interface
{
    public interface IUserRolesRepository
    {
        IEnumerable<int> GetById(int id);
        bool AddRole(UserRole userRole);
        bool RemoveRole(UserRole userRole);
    }
}
