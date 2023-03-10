namespace OnlineTest.Model.Interface
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers(int next, int? limit = null);
        User GetByEmail(string email);
        Task<bool> AddUserAsync(User user);
        Task<bool> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(User user);
        IEnumerable<User> SeachUser(int? id = null,string? name = null, string? email=null, string? mobile = null, bool? isactive = null);
        bool ActiveUser(int id, bool isactive);
        bool ChangePassword(int id ,string oldpassword, string password);
    }
}
