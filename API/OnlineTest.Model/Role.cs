using System.ComponentModel.DataAnnotations;

namespace OnlineTest.Model
{
    public class Role
    {
        public int Id { get; set; }
        [MaxLength(32)]
        public string RoleName { get; set; }
    }
}
