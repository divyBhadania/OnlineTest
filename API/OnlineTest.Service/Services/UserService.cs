using Newtonsoft.Json;
using OnlineTest.Model;
using OnlineTest.Model.Interface;
using OnlineTest.Service.DTO;
using OnlineTest.Service.Interface;


namespace OnlineTest.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<UserDTO> GetUsers()
        {
            var users = _userRepository.GetUsers().Select(s => new UserDTO()
            {
                Name = s.Name,
                Email = s.Email,
                MobileNo = s.MobileNo,
                Password = s.Password,
                Id = s.Id,
                IsActive = s.IsActive,
            }).ToList();
            return users;

        }
        public bool AddUser(UserDTO user)
        {
            return _userRepository.AddUser(new User
            {
                Email = user.Email,
                MobileNo = user.MobileNo,
                Name = user.Name,
                Password = user.Password
            }); ;

        }

        public bool UpdateUser(UserDTO user)
        {
            return _userRepository.UpdateUser(new User
            {
                Id = user.Id,
                Email = user.Email,
                MobileNo = user.MobileNo,
                Name = user.Name,
                Password = user.Password,
                IsActive = user.IsActive
            }); ;
        }
        public bool DeleteUser(UserDTO user)
        {
            return _userRepository.DeleteUser(new User
            {
                Id = user.Id,
                Email = user.Email,
                MobileNo = user.MobileNo,
                Name = user.Name,
                Password = user.Password,
                IsActive = user.IsActive
            }); ;
        }

        public List<UserDTO> SeachUser(int? id = null, string? name = null, string? email = null, string? mobile = null, bool? isactive = null)
        {
            var userDTO = new List<UserDTO>();
            var data = _userRepository.SeachUser(id, name, email, mobile, isactive);
            if (data == null) {
                return new List<UserDTO>();
            }
            foreach (var i in data)
            {
                userDTO.Add(new UserDTO
                {
                    Id = i.Id,
                    Email = i.Email,
                    MobileNo = i.MobileNo,
                    Name = i.Name,
                    Password = i.Password,
                    IsActive = i.IsActive
                });
            }
            return userDTO;

        }

        public bool ActiveUser(int id, bool isactive)
        {
            return _userRepository.ActiveUser(id , isactive); 
        }

        public bool ChangePassword(int id,string oldpassword, string password)
        {
            return _userRepository.ChangePassword(id, oldpassword, password);
        }
    }
}