using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTest.Model.Interface
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
        bool AddUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(User user);
        IEnumerable<User> SeachUser(int? id = null,string? name = null, string? email=null, string? mobile = null, bool? isactive = null);
        bool ActiveUser(int id, bool isactive);
        bool ChangePassword(int id ,string oldpassword, string password);
    }
}
