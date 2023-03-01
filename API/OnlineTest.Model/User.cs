using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTest.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(32)]
        public string Name { get; set; }
        public string Password { get; set; }
        [MaxLength(32)]
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public bool IsActive { get; set; }
    }
}
