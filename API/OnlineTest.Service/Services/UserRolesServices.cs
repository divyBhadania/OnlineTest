using OnlineTest.Model;
using OnlineTest.Model.Interface;
using OnlineTest.Model.Repository;
using OnlineTest.Service.DTO;
using OnlineTest.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTest.Service.Services
{
    public class UserRolesServices : IUserRolesServices
    {
        private readonly IUserRolesRepository _userRolesServices;

        public UserRolesServices(IUserRolesRepository userRolesServices)
        {
            _userRolesServices = userRolesServices;
        }
        public List<int> GetById(int id)
        {
            return _userRolesServices.GetById(id).ToList();
        }
        public bool AddRole(UserRolesDTO userRole)
        {
            return _userRolesServices.AddRole(new UserRole
            {
                UserId = userRole.Id,
                RoleId = userRole.RoleId
            });
        }
        public bool RemoveRole(UserRolesDTO userRole)
        {
            return _userRolesServices.RemoveRole(new UserRole
            {
                UserId = userRole.Id,
                RoleId = userRole.RoleId
            });
        }
    }
}
