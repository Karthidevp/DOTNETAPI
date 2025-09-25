using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class UserModels
    {
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? EmailId { get; set; }
        public string? Address { get; set; }
        public string? Password { get; set; }
        public string? Status { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
        public string? PhoneNo { get; set; }
        public int RoleId { get; set; }
        public bool IsDeleted { get; set; }
        public bool HasOrder { get; set; }
    }
}
