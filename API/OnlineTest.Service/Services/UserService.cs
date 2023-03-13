using AutoMapper;
using OnlineTest.Model;
using OnlineTest.Model.Interface;
using OnlineTest.Service.DTO;
using OnlineTest.Service.Interface;
using OnlineTest.Services.DTO;

namespace OnlineTest.Service.Services
{
    public class UserService : IUserServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserRolesRepository _userRolesRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository , IUserRolesRepository userRolesRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _userRolesRepository = userRolesRepository;
            _mapper = mapper;
        }
        public List<UserDTO> GetUsers(int page, int? limit = null)
        {
            var data = new List<UserDTO>();
            _userRepository.GetUsers(page, limit).ToList().ForEach(item => data.Add(_mapper.Map<UserDTO>(item)));
            return data;

        }
        public bool AddUser(UserDTO user)
        {
            if (_userRepository.AddUserAsync(_mapper.Map<User>(user)).Result)
            {
                if(_userRolesRepository.AddRole(new UserRole
                {
                    UserId = _userRepository.SeachUser(email: user.Email).Select(i => i.Id).FirstOrDefault(),
                    RoleId = 3
                }))
                    return true;

                else
                {
                    var data = _userRepository.SeachUser(email: user.Email).FirstOrDefault();
                    if(data != null)
                    {
                        _userRepository.DeleteUserAsync(data);
                        return false;
                    }
                }
            }
            return false;
        }
        public bool UpdateUser(UserDTO user)
        {
            return _userRepository.UpdateUserAsync(_mapper.Map<User>(user)).Result;
        }
        public bool DeleteUser(UserDTO user)
        {
            return _userRepository.DeleteUserAsync(_mapper.Map<User>(user)).Result;
        }
        public List<UserDTO> SeachUser(int? id = null, string? name = null, string? email = null, string? mobile = null, bool? isactive = null)
        {
            var data = new List<UserDTO>();
            _userRepository.SeachUser(id, name, email, mobile, isactive)
                .ToList()
                .ForEach(item => data.Add(_mapper.Map<UserDTO>(item)));
            return data;
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
            var user = _userRepository.GetByEmail(model.Username);
            if (user == null || !(user.Password ==model.Password))
                return null;
            return new UserDTO()
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
            };
        }
    }
}