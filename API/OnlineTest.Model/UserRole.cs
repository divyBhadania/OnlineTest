using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineTest.Model
{
    public class UserRole
    {
        public int Id { get; set; }
        [Column("User_Id")]
        public int UserId { get; set; }
        [Column("Role_Id")]
        public int RoleId { get; set; }
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}
