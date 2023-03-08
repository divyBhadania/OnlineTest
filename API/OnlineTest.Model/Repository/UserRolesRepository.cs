using OnlineTest.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTest.Model.Repository
{
    public class UserRolesRepository : IUserRolesRepository
    {
        private readonly OnlineTestContext _context;
        public UserRolesRepository(OnlineTestContext context)
        {
            _context = context;
        }

        public bool AddRole(UserRole userRole)
        {
            _context.UserRoles.Add(userRole);
            return _context.SaveChanges() > 0; 
        }

        public IEnumerable<int> GetById(int id)
        {
            return _context.UserRoles.Where(i => i.UserId== id).Select(i => i.RoleId);
        }

        public bool RemoveRole(UserRole userRole)
        {
            _context.UserRoles.Remove(userRole);
            return _context.SaveChanges() > 0;
        }
    }
}
