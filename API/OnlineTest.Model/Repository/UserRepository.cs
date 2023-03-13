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
        public IEnumerable<User> GetUsers(int page, int? limit = null)
        {
            return limit == null ? _context.Users : _context.Users.Skip((page - 1) * (int)limit).Take((int)limit);
        }
        public async Task<bool> AddUserAsync(User user)
        {
            _context.Users.Add(user);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteUserAsync(User user)
        {
            _context.Users.Remove(user);
            return await _context.SaveChangesAsync() > 0;
        }

        public IEnumerable<User> SeachUser(int? id = null, string? name = null, string? email = null, string? mobile = null, bool? isactive = null)
        {
            if (id != null || name != null || email != null || mobile != null || isactive != null)
                return _context.Users.Where(i => (id != null ? i.Id.ToString().Contains(id.ToString()) : true)).
                        Where(i => (name != null ? i.Name.Contains(name) : true)).
                        Where(i => (email != null ? i.Email.Contains(email) : true)).
                        Where(i => (mobile != null ? i.MobileNo.Contains(mobile) : true)).
                        Where(i => (isactive != null ? i.IsActive == isactive : true));
            //return from e in _context.Users
            //           where (id != null ? e.Id == id : true) &&
            //           (name != null ? e.Name == name : true) &&
            //           (email != null ? e.Email == email : true) &&
            //           (mobile != null ? e.MobileNo == mobile : true) &&
            //           (isactive != null ? e.IsActive == isactive : true)
            //           select e;
            return null;
        }

        public bool ActiveUser(int id, bool isactive)
        {
            _context.Entry(new User { Id = id, IsActive = isactive }).Property(i => i.IsActive).IsModified = true;
            return _context.SaveChanges() > 0;
        }

        public bool ChangePassword(int id, string oldpassword, string password)
        {
            if (_context.Users.Where(i => i.Id == id).Where(i => i.Password == oldpassword).Any())
            {
                _context.Entry(new User { Id = id, Password = password }).Property(i => i.Password).IsModified = true;
                return _context.SaveChanges() > 0;
            }
            return false;
        }

        public User GetByEmail(string email)
        {
            return _context.Users.Where(i => i.Email == email).FirstOrDefault();
        }
    }
}