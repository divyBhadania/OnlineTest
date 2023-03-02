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
        bool UpdateUser(UserDTO user);

        bool DeleteUser(UserDTO user);
        List<UserDTO> SeachUser(int? id = null, string? name = null, string? email = null, string? mobile = null, bool? isactive = null);
    }
}
