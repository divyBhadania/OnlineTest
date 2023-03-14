using OnlineTest.Model;
using OnlineTest.Service.DTO;
using OnlineTest.Services.DTO;

namespace OnlineTest.Service.Interface
{
    public interface IUserServices
    {
        ResponseDTO GetUsers(int next, int? limit = null);
        ResponseDTO AddUser(AddUserDTO user);
        ResponseDTO UpdateUser(AddUserDTO user);

        ResponseDTO DeleteUser(int id);
        List<AddUserDTO> SeachUser(int? id = null, string? name = null, string? email = null, string? mobile = null, bool? isactive = null);
        bool ActiveUser(int id, bool isactive);
        bool ChangePassword(int id, string oldpassword, string password);
        AddUserDTO IsUserExists(TokenDTO model);
    }
}
