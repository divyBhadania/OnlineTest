using OnlineTest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTest.Service.Interface
{
    public interface IUserService
    {
        List<User> GetAllUser();
        User GetUserById(int id);
        void AddUser(User user);
        void DeleteUserById(User user);
        void UpdateUser(User user);
    }
}
