using OnlineTest.Model;
using OnlineTest.Service.DTO;
using OnlineTest.Services.DTO;

namespace OnlineTest.Service.Interface
{
    public interface IUserService
    {
        List<UserDTO> GetUsers(int next, int? limit = null);
        bool AddUser(UserDTO user);
        bool UpdateUser(UserDTO user);

        bool DeleteUser(UserDTO user);
        List<UserDTO> SeachUser(int? id = null, string? name = null, string? email = null, string? mobile = null, bool? isactive = null);
        bool ActiveUser(int id, bool isactive);
        bool ChangePassword(int id, string oldpassword, string password);
        UserDTO IsUserExists(TokenDTO model);
    }
}
