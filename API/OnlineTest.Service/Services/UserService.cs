using AutoMapper;
using OnlineTest.Model;
using OnlineTest.Model.Interface;
using OnlineTest.Service.DTO;
using OnlineTest.Service.Interface;
using OnlineTest.Services.DTO;
using static System.Net.Mime.MediaTypeNames;

namespace OnlineTest.Service.Services
{
    public class UserService : IUserServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserRolesRepository _userRolesRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IUserRolesRepository userRolesRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _userRolesRepository = userRolesRepository;
            _mapper = mapper;
        }
        public ResponseDTO GetUsers(int page, int? limit = null)
        {
            try
            {
                var data = _mapper.Map<List<GetUserDTO>>(_userRepository.GetUsers(page, limit).ToList());
                if (data.Count > 0)
                    return new ResponseDTO
                    {
                        Status = 200,
                        Data = data,
                        Message = "All Users"
                    };

                else
                    return new ResponseDTO
                    {
                        Status = 404,
                        Message = "No user found"
                    };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    Status = 400,
                    Message = "Failed",
                    Error = ex.Message
                };
            }
        }
        public ResponseDTO AddUser(AddUserDTO user)
        {
            try
            {
                if(_userRepository.SeachUser(email : user.Email).ToList().Count > 0)
                    return new ResponseDTO
                    {
                        Status = 400,
                        Message = "Email id already exits."
                    };
                if (_userRepository.AddUserAsync(_mapper.Map<User>(user)).Result)
                {
                    if (_userRolesRepository.AddRole(new UserRole
                    {
                        UserId = _userRepository.SeachUser(email: user.Email).Select(i => i.Id).FirstOrDefault(),
                        RoleId = 3
                    }))
                        return new ResponseDTO
                        {
                            Status = 200,
                            Message = "User added sucessfully"
                        };

                    else
                    {
                        var data = _userRepository.SeachUser(email: user.Email).FirstOrDefault();
                        if (data != null)
                        {
                            if (_userRepository.DeleteUserAsync(data).Result)
                                return new ResponseDTO
                                {
                                    Status = 400,
                                    Message = "Error in adding role. User not added."
                                };
                        }
                    }
                }
                return new ResponseDTO
                {
                    Status = 400,
                    Message = "User not added"
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    Status = 400,
                    Message = "Failed",
                    Error = ex.Message
                };
            }
        }
        public ResponseDTO UpdateUser(AddUserDTO user)
        {
            try
            {
                var data = _userRepository.SeachUser(email : user.Email).ToList();
                if(data.Count != 0 && data.FirstOrDefault().Id != user.Id)
                    return new ResponseDTO
                    {
                        Status = 400,
                        Message = "Email id already exits with different account."
                    };
                if(_userRepository.SeachUser(id : user.Id).ToList().Count == 0)
                    return new ResponseDTO
                    {
                        Status = 400,
                        Message = "Invalid User Id."
                    };
                if(_userRepository.UpdateUserAsync(_mapper.Map<User>(user)).Result)
                    return new ResponseDTO
                    {
                        Status = 200,
                        Message = "User update successfully"
                    };
                else
                    return new ResponseDTO
                    {
                        Status = 200,
                        Message = "User data Not updated."
                    };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    Status = 400,
                    Message = "Failed",
                    Error = ex.Message
                };
            }
        }
        public ResponseDTO DeleteUser(int id)
        {
            try
            {
                var user = _userRepository.SeachUser(id: id).FirstOrDefault();
                if(user == null)
                    return new ResponseDTO
                    {
                        Status = 400,
                        Message = "No User Found."
                    };
                if(_userRepository.DeleteUserAsync(user).Result)
                    return new ResponseDTO
                    {
                        Status = 200,
                        Message = "User deleted successfully."
                    };
                else
                    return new ResponseDTO
                    {
                        Status = 400,
                        Message = "User Not deleted"
                    };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    Status = 400,
                    Message = "Failed",
                    Error = ex.Message
                };
            }
        }
        public List<AddUserDTO> SeachUser(int? id = null, string? name = null, string? email = null, string? mobile = null, bool? isactive = null)
        {
            var data = new List<AddUserDTO>();
            _userRepository.SeachUser(id, name, email, mobile, isactive)
                .ToList()
                .ForEach(item => data.Add(_mapper.Map<AddUserDTO>(item)));
            return data;
        }
        public bool ActiveUser(int id, bool isactive)
        {
            return _userRepository.ActiveUser(id, isactive);
        }
        public bool ChangePassword(int id, string oldpassword, string password)
        {
            return _userRepository.ChangePassword(id, oldpassword, password);
        }
        public AddUserDTO IsUserExists(TokenDTO model)
        {
            var user = _userRepository.GetByEmail(model.Username);
            if (user == null || !(user.Password == model.Password))
                return null;
            return new AddUserDTO()
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
            };
        }
    }
}