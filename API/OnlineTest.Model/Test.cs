using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineTest.Model
{
    public class Test
    {
        public int Id { get; set; }
        public string TestName { get; set; }
        public string Description { get; set; }
        [ForeignKey("UserCreatedBy")]
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ExpireOn { get; set; }
        public int TechnologyId { get; set; }
        public User UserCreatedBy { get; set; }
        public Technology Technology { get; set; }
    }
}
