using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTest.Model.Interface
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers();
        Task<bool> AddUser(User user);

        Task<bool> UpdateUser(User user);
        bool DeleteUser(User user);
    }
}
