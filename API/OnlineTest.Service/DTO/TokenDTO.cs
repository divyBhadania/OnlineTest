using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTest.Services.DTO
{
    public class TokenDTO
    {
        public string Grant_Type { get; set; }
        public string Refresh_Token { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
