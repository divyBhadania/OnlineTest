using Microsoft.EntityFrameworkCore;
using OnlineTest.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTest.Model.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly OnlineTestContext _context;
        public UserRepository(OnlineTestContext context)
        {
            _context = context;
        }
        public async Task<bool> AddUser(User user)
        {
             _context.Users.Add(user);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<User>>  GetUsers()
        {
            return await _context.Users.ToListAsync(); ;
        }

        public async Task<bool> UpdateUser(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public bool DeleteUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
