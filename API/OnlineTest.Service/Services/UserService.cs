using Microsoft.EntityFrameworkCore;
using OnlineTest.Model;
using OnlineTest.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTest.Service.Services
{
    public class UserService : IUserService
    {
        private OnlineTestContext _context;
        public UserService(OnlineTestContext context)
        {
            _context = context;
        }

        public List<User> GetAllUser()
        {
            return _context.Users.ToList();
        }

        public User GetUserById(int id)
        {
            return _context.Users.Where(x => x.UserId.Equals(id)).FirstOrDefault();
        }

        public void UpdateUser(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteUserById(User user)
        {
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

    }
}
