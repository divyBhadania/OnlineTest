using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTest.Model
{
    public class Test
    {
        public int Id { get; set; }
        public string TestName { get; set; }
        public string Description { get; set; }
        [ForeignKey("UserCreatedBy")]
        public int CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime ExpireOn { get; set; }
        public int TechnologyId { get; set; }
        public User UserCreatedBy { get; set; }
        public Technology Technology { get; set; }
    }
}
