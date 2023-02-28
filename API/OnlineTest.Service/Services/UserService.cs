using Microsoft.EntityFrameworkCore;
using OnlineTest.Model;
using OnlineTest.Model.Interface;
using OnlineTest.Service.DTO;
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
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool AddUser(UserDTO user)
        {
            _userRepository.AddUser(new User { Email = user.Email, MobileNo = user.MobileNo, Name = user.Name, Password = user.Password });
            return true;
        }

        public List<UserDTO> GetUsers()
        {
            try
            {
                var users = _userRepository.GetUsers().Select(s => new UserDTO()
                {
                    Name = s.Name,
                    Email = s.Email,
                    MobileNo = s.MobileNo,
                    Password = s.Password,
                    UserId = s.UserId
                }).ToList();
                return users;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateUser(UserDTO user)
        {
            _userRepository.UpdateUser(new User { Email = user.Email, MobileNo = user.MobileNo, Name = user.Name, Password = user.Password });
            return true;
        }
    }
}
