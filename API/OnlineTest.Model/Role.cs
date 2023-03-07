using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineTest.Model
{
    public class Role
    {
        public int Id { get; set; }
        [MaxLength(32)]
        [Column("Role_Name")]
        public string RoleName { get; set; }
    }
}
