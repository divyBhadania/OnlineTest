using OnlineTest.Model;
using OnlineTest.Model.Interface;
using OnlineTest.Service.DTO;
using OnlineTest.Service.Interface;
using OnlineTest.Services.DTO;

namespace OnlineTest.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<UserDTO> GetUsers(int page, int? limit = null)
        {
            var users = _userRepository.GetUsers(page, limit).Select(s => new UserDTO()
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
            }); 

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
            var data = _userRepository.SeachUser(id, name, email, mobile, isactive).ToList();
            if (data.Count ==0) {
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

        public UserDTO IsUserExists(TokenDTO model)
        {
            var user = (_userRepository.GetUsers(1,null).FirstOrDefault(x => x.Email.ToLower() == model.Username.ToLower() && x.Password == model.Password));
            if (user == null)
                throw new Exception("User not found ..!!");
            return new UserDTO()
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
            };
        }
    }
}