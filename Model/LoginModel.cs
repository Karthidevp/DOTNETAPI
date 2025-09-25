using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class LoginModel
    {
        public string? EmailId { get; set; }
        public string? Password { get; set; }
    }
    public class LoginResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public string? Role { get; set; }

        public int LoginstatusId { get; set; }
        public int Id { get; set; }
    }
}
