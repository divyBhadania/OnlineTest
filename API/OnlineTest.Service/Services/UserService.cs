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
            try
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
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool AddUser(UserDTO user)
        {
            _userRepository.AddUser(new User { Email = user.Email, MobileNo = user.MobileNo, Name = user.Name, Password = user.Password ,IsActive=user.IsActive });
            return true;
        }

        public bool UpdateUser(UserDTO user)
        {
            _userRepository.UpdateUser(new User { Id = user.Id, Email = user.Email, MobileNo = user.MobileNo, Name = user.Name, Password = user.Password, IsActive = user.IsActive });
            return true;
        }
        public bool DeleteUser(UserDTO user)
        {
            _userRepository.DeleteUser(new User { Id = user.Id, Email = user.Email, MobileNo = user.MobileNo, Name = user.Name, Password = user.Password, IsActive = user.IsActive });
            return true;
        }
    }
}