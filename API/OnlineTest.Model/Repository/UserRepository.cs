using Microsoft.EntityFrameworkCore;
using OnlineTest.Model.Interface;
using System.Collections.Immutable;

namespace OnlineTest.Model.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly OnlineTestContext _context;
        public UserRepository(OnlineTestContext context)
        {
            _context = context;
        }
        public IEnumerable<User>  GetUsers()
        {
            return _context.Users.ToList();
        }
        public bool AddUser(User user)
        {
             _context.Users.Add(user);
            return _context.SaveChanges() > 0;
        }

        public bool UpdateUser(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
            return true;
        }

        public bool DeleteUser(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
            return true;
        }

        public User SeachUser(int? id=null, string? email = null)
        {
            return _context.Users.Where(i => (id != null ? i.Id == id : true)).
                    Where(i => (email != null ? i.Email==email : true)).
                    FirstOrDefault();

            //return (User)(from e in _context.Users.ToImmutableArray()
            //              where (email != null ? e.Email == email : true) && e.Id == id
            //              select e);
            //var data = (from e in _context.Users
            //                  where e.Id == id
            //                  select e);
            //return (User)data;
        }
    }
}