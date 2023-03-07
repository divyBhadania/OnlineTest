using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OnlineTest.Model
{
    public class RToken
    {
        [Key]
        public int Id { get; set; }
        [Column("Refresh_Token", TypeName = "varchar(150)")]
        public string RefreshToken { get; set; }
        [Column("Is_Stop")]
        public int IsStop { get; set; }
        [Column("Created_Date")]
        public DateTime CreatedDate { get; set; }
        [Column("User_Id")]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User UserNavigation { get; set; }
    }
}
