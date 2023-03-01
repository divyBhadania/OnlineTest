using Microsoft.EntityFrameworkCore;
using OnlineTest.Model.Interface;
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

        public User SeachUser(int id, int? mobile = null)
        {
            throw new NotImplementedException();
        }
    }
}