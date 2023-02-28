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

    }
}
