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
            return _context.Users;
        }
        public bool AddUser(User user)
        {
            _context.Users.Add(user);
            return _context.SaveChanges() > 0;
        }

        public bool UpdateUser(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            return _context.SaveChanges() > 0;
        }

        public bool DeleteUser(User user)
        {
            _context.Users.Remove(user);
            return _context.SaveChanges() >0 ;
        }

        public IEnumerable<User> SeachUser(int? id = null, string? name = null, string? email = null, string? mobile = null, bool? isactive = null)
        {
            return _context.Users.Where(i => (id != null ? i.Id.ToString().Contains(id.ToString()) : true)).
                    Where(i => (name != null ? i.Name.Contains(name) : true)).
                    Where(i => (email != null ? i.Email.Contains(email) : true)).
                    Where(i => (mobile != null ? i.MobileNo.Contains(mobile) : true)).
                    Where(i => (isactive != null ? i.IsActive == isactive : true));
                   

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