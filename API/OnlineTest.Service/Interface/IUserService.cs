using OnlineTest.Model;
using OnlineTest.Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTest.Service.Interface
{
    public interface IUserService
    {
        List<UserDTO> GetUsers();
        bool AddUser(UserDTO user);
    }
}
